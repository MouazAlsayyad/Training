using MediatR;

namespace Training.Application.Features.Options.Commands.CreateOption
{
    public class CreateOptionCommand : IRequest<CreateOptionCommandResponse>
    {
        public required string Text { get; set; }

        public bool? IsCorrect { get; set; }

        public Guid QuestionId { get; set; }
    }
}
