using Microsoft.EntityFrameworkCore;
using Training.Application.Contracts.Persistence;
using Training.Domain.Entities;

namespace Training.Persistence.Repositories
{
    public class QuestionRepository(TrainingDbContext dbContext) : BaseRepository<Question>(dbContext), IQuestionRepository
    {
    }
}
