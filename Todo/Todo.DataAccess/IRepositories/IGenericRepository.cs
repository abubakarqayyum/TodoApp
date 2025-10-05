using System.Linq.Expressions;
using Todo.Entities;

namespace Todo.DataAccess.IRepositories
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<(IReadOnlyList<TEntity> Items, int TotalCount)> GetListAsync(PaginationDto search, long userId, Expression<Func<TEntity, bool>>? predicate = null);
        Task<TEntity?> GetByIdAsync(object id);
        Task<TEntity> AddAsync(TEntity entity);
        Task<TEntity> UpdateAsync(TEntity entity);
        Task DeleteAsync(object id, long? deletedBy = null);
    }
}


