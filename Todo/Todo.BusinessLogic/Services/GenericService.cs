using AutoMapper;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Todo.BusinessLogic.IServices;
using Todo.DataAccess.IRepositories;
using Todo.Entities;
using Todo.Utilities.Exceptions;

namespace Todo.BusinessLogic.Services
{
    public class GenericService<TEntity, TReadDto, TCreateUpdateDto>: IGenericService<TEntity, TReadDto, TCreateUpdateDto> where TEntity : BaseEntity
    {
        private readonly IGenericRepository<TEntity> _repository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GenericService(IGenericRepository<TEntity> repository, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            _repository = repository;
            _mapper = mapper;
            _httpContextAccessor = httpContextAccessor;

        }

        public async Task<(IReadOnlyList<TReadDto> Items, int TotalCount)> GetListAsync(PaginationDto search)
        {
            var (entities, total) = await _repository.GetListAsync(search, Convert.ToInt64(GetCurrentUserId()));
            var dtos = _mapper.Map<IReadOnlyList<TReadDto>>(entities);
            return (dtos, total);
        }

        public async Task<TReadDto> GetByIdAsync(object id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null) throw new NotFoundException($"{typeof(TEntity).Name} not found");
            return _mapper.Map<TReadDto>(entity);
        }

        public async Task<TReadDto> AddAsync(TCreateUpdateDto dto)
        {
            var entity = _mapper.Map<TEntity>(dto);

            var currentUserId = GetCurrentUserId();
            if (currentUserId.HasValue)
                entity.CreatedBy = currentUserId.Value;

            entity = await _repository.AddAsync(entity);
            return _mapper.Map<TReadDto>(entity);
        }

        public async Task<TReadDto> UpdateAsync(object id, TCreateUpdateDto dto)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) throw new NotFoundException($"{typeof(TEntity).Name} not found");

            _mapper.Map(dto, existing);

            var currentUserId = GetCurrentUserId();
            if (currentUserId.HasValue)
                existing.ModifiedBy = currentUserId.Value;

            var updated = await _repository.UpdateAsync(existing);
            return _mapper.Map<TReadDto>(updated);
        }

        public async Task DeleteAsync(object id)
        {
            var existing = await _repository.GetByIdAsync(id);
            if (existing == null) throw new NotFoundException($"{typeof(TEntity).Name} not found");

            var currentUserId = GetCurrentUserId();
            await _repository.DeleteAsync(id, currentUserId);
        }

        #region Private-Methods
        private long? GetCurrentUserId()
        {
            var userId = _httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (int.TryParse(userId, out var id))
                return id;
            return null;
        }
        #endregion
    }

}


