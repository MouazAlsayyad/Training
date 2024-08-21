using Training.Application.Responses;

namespace Training.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommandResponse : BaseResponse
    {
        public CreateQuestionCommandResponse() : base() { }
        public CreateQuestionDto Question { get; set; } = default!;
    }
}
