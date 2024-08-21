using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Training.Application.Contracts.Persistence;

namespace Training.Persistence.Repositories
{
    public class BaseRepository<T>(TrainingDbContext dbContext) : IAsyncRepository<T> where T : class
    {
        protected readonly TrainingDbContext _dbContext = dbContext;

        public virtual async Task<T?> GetByIdAsync(Guid id, CancellationToken? cancellationToken = null)
        {
            return await _dbContext.Set<T>()
                .FindAsync([id], cancellationToken ?? CancellationToken.None);
        }

        public virtual async Task<IReadOnlyList<T>> ListAllAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken? cancellationToken = null)
        {
            IQueryable<T> query = _dbContext.Set<T>().AsNoTracking();

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            return await query.ToListAsync(cancellationToken ?? CancellationToken.None);
        }

        public virtual async Task<IReadOnlyList<T>> GetPagedResponseAsync(int page, int size, CancellationToken? cancellationToken = null)
        {
            return await _dbContext.Set<T>()
                .Skip((page - 1) * size)
                .Take(size)
                .AsNoTracking()
                .ToListAsync(cancellationToken ?? CancellationToken.None);
        }

        public virtual async Task<T> AddAsync(T entity, CancellationToken? cancellationToken = null)
        {
            await _dbContext.Set<T>().AddAsync(entity, cancellationToken ?? CancellationToken.None);
            await _dbContext.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
            return entity;
        }

        public virtual async Task UpdateAsync(T entity, CancellationToken? cancellationToken = null)
        {
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
        }

        public virtual async Task DeleteAsync(T entity, CancellationToken? cancellationToken = null)
        {
            _dbContext.Set<T>().Remove(entity);
            await _dbContext.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
        }
    }
}
