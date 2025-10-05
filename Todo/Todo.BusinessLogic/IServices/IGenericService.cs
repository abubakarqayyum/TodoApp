using Todo.Entities;

namespace Todo.BusinessLogic.IServices
{
    public interface IGenericService<TEntity, TReadDto, TCreateUpdateDto> where TEntity : class
    {
        Task<(IReadOnlyList<TReadDto> Items, int TotalCount)> GetListAsync(PaginationDto search);
        Task<TReadDto> GetByIdAsync(object id);
        Task<TReadDto> AddAsync(TCreateUpdateDto dto);
        Task<TReadDto> UpdateAsync(object id, TCreateUpdateDto dto);
        Task DeleteAsync(object id);
    }
}