using CommonService.Dtos;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Todo.BusinessLogic.Dtos.User;
using Todo.BusinessLogic.IServices;

namespace Todo.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("register")]
        public async Task<ActionResult<OperationResponse>> RegisterAsync([FromBody] UserRegisterDto dto,[FromServices] IValidator<UserRegisterDto> validator)
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                return BadRequest(new OperationResponse(false, 400, "Validation failed",
                    validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })));
            }
            await _userService.RegisterAsync(dto);
            return StatusCode(201, new OperationResponse(true, 201, "User registered successfully", null));
        }

        [HttpPost("login")]
        public async Task<ActionResult<OperationResponse>> LoginAsync([FromBody] UserLoginDto dto,[FromServices] IValidator<UserLoginDto> validator)
        {
            var validation = await validator.ValidateAsync(dto);
            if (!validation.IsValid)
            {
                return BadRequest(new OperationResponse(false, 400, "Validation failed",
                    validation.Errors.Select(e => new { e.PropertyName, e.ErrorMessage })));
            }
            var token = await _userService.LoginAsync(dto);
            return Ok(new OperationResponse(true, 200, "Login successful", new { Token = token }));
        }
    }
}
