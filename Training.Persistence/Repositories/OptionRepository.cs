using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Training.Application.Contracts.Persistence;
using Training.Domain.Entities;

namespace Training.Persistence.Repositories
{
    public class OptionRepository(TrainingDbContext dbContext) : BaseRepository<Option>(dbContext), IOptionRepository
    {
        public async Task DeleteAllOptionsAsync(Expression<Func<Option, bool>> predicate, CancellationToken? cancellationToken = null)
        {
            var optionsToDelete = await _dbContext.Set<Option>()
                .Where(predicate)
                .ToListAsync(cancellationToken ?? CancellationToken.None);

            if (optionsToDelete.Count != 0)
            {
                _dbContext.Set<Option>().RemoveRange(optionsToDelete);
                await _dbContext.SaveChangesAsync(cancellationToken ?? CancellationToken.None);
            }
        }
    }
}
