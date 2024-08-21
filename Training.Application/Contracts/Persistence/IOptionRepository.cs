using System.Linq.Expressions;
using Training.Domain.Entities;

namespace Training.Application.Contracts.Persistence
{
    public interface IOptionRepository : IAsyncRepository<Option>
    {
        Task DeleteAllOptionsAsync(Expression<Func<Option, bool>> predicate, CancellationToken? cancellationToken);
    }
}
