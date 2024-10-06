using MediatR;
using Training.Application.Responses;

namespace Training.Application.Features.Options.Commands.CreateOption
{
    public class CreateOptionCommand : IRequest<BaseResponse<Guid>>
    {
        public required string Text { get; set; }

        public bool? IsCorrect { get; set; }

        public Guid QuestionId { get; set; }
    }
}
