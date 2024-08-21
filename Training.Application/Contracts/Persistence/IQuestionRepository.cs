using System.Linq.Expressions;
using Training.Domain.Entities;

namespace Training.Application.Contracts.Persistence
{
    public interface IQuestionRepository: IAsyncRepository<Question>
    {
    }
}
