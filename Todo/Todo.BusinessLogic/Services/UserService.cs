using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Todo.BusinessLogic.Dtos.User;
using Todo.BusinessLogic.IServices;
using Todo.DataAccess.IRepositories;
using Todo.Entities.Entity;
using Todo.Utilities.Dtos;
using Todo.Utilities.Encryption;
using Todo.Utilities.Exceptions;

namespace Todo.BusinessLogic.Services
{
    public class UserService : GenericService<User, UserReadDto, UserRegisterDto>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly JwtOptions _jwtOptions;

        public UserService(
            IUserRepository userRepository,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            IOptions<JwtOptions> jwtOptions)
            : base(userRepository, mapper, httpContextAccessor)
        {
            _userRepository = userRepository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            _jwtOptions = jwtOptions.Value;
        }

        public async Task<bool> RegisterAsync(UserRegisterDto dto)
        {
            var existing = await _userRepository.GetByEmailAsync(dto.Email);

            if (existing != null)
                throw new BadRequestException("Email already registered");

            var entity = _mapper.Map<User>(dto);
            entity.PasswordHash = Encryption.Encrypt(dto.Password);

            var created = await _userRepository.AddAsync(entity);
            return true;
        }

        public async Task<string> LoginAsync(UserLoginDto dto)
        {
            var user = await _userRepository.GetByEmailAndPasswordAsync(dto.Email, Encryption.Encrypt(dto.Password));
            if (user == null)
                   throw new UnauthorizedAccessException("Invalid email or password.");
           return GenerateJwtToken(user);
        }

        #region Private-Functions
        private string GenerateJwtToken(User user)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Secret));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                new Claim(ClaimTypes.Email, user.Email ?? string.Empty),
                new Claim(ClaimTypes.Name, user.FullName ?? string.Empty)
            };

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtOptions.TokenLifetime),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        #endregion

    }
}
