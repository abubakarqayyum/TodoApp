using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Todo.DataAccess.IRepositories;
using Todo.Entities;

namespace Todo.DataAccess.Repositories
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity>
        where TEntity : BaseEntity
    {
        private readonly AppDbContext _dbContext;
        private readonly DbSet<TEntity> _dbSet;

        public GenericRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public async Task<(IReadOnlyList<TEntity> Items, int TotalCount)> GetListAsync(PaginationDto search,long userId, Expression<Func<TEntity, bool>>? predicate = null)
        {
            IQueryable<TEntity> query = _dbSet.AsNoTracking().Where(x => !x.IsDeleted && x.IsActive && x.CreatedBy == userId);
            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            var total = await query.CountAsync();

            var items = await query
                .Skip((search.pageNo - 1) * search.pageSize)
                .Take(search.pageSize)
                .ToListAsync();
            return (items, total);
        }

        public async Task<TEntity?> GetByIdAsync(object id)
        {
            return await _dbSet.FirstOrDefaultAsync(x => x.Id == Convert.ToInt64(id) && !x.IsDeleted && x.IsActive);
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            entity.CreatedDate = DateTime.UtcNow;
            entity.IsActive = true;
            await _dbSet.AddAsync(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            entity.ModifiedDate = DateTime.UtcNow;
            _dbSet.Update(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task DeleteAsync(object id, long? deletedBy = null)
        {
            var entity = await GetByIdAsync(id);
            if (entity != null)
            {
                entity.IsDeleted = true;
                entity.IsActive = false;
                entity.DeletedDate = DateTime.UtcNow;
                if (deletedBy.HasValue)
                    entity.DeletedBy = deletedBy.Value;
                _dbSet.Update(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
