using MediatR;
using Training.Application.Responses;
namespace Training.Application.Features.Questions.Commands.DeleteQuestion
{
    public class DeleteQuestionCommand : IRequest<BaseResponse<object>>
    {
        public Guid QuestionId { get; set; }
    }
}
