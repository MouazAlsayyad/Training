using Training.Application.Contracts.Persistence;
using Training.Domain.Entities;

namespace Training.Persistence.Repositories
{
    public class QuestionBankRepository(TrainingDbContext dbContext) : BaseRepository<QuestionBank>(dbContext), IQuestionBankRepository
    {
    }
}
