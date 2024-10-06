using System.Linq.Expressions;

namespace Training.Application.Contracts.Persistence
{
    public interface IAsyncRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(Guid id, CancellationToken? cancellationToken = null);
        Task<T?> GetByIdAsync(int id, CancellationToken? cancellationToken = null);
        Task<IReadOnlyList<T>> ListAllAsync(Expression<Func<T, bool>>? predicate = null, CancellationToken? cancellationToken = null);
        Task<IReadOnlyList<T>> GetPagedResponseAsync(int page, int size, CancellationToken? cancellationToken = null);
        Task<T> AddAsync(T entity, CancellationToken? cancellationToken = null);
        Task UpdateAsync(T entity, CancellationToken? cancellationToken = null);
        Task DeleteAsync(T entity, CancellationToken? cancellationToken = null);
        IQueryable<T> Where(Expression<Func<T, bool>> predicate);
    }
}
