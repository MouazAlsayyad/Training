using Training.Application.Responses;

namespace Training.Application.Features.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionCommandResponse:BaseResponse
    {
        public UpdateQuestionCommandResponse() : base() { }
        public UpdateQuestionDto Question { get; set; } = default!;
    }
}
