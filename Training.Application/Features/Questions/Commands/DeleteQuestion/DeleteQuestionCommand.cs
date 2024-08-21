using MediatR;
namespace Training.Application.Features.Questions.Commands.DeleteQuestion
{
    public class DeleteQuestionCommand : IRequest<DeleteQuestionCommandResponse>
    {
        public Guid QuestionId { get; set; }
    }
}
