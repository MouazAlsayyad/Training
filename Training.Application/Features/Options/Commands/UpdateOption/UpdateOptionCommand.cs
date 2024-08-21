using MediatR;

namespace Training.Application.Features.Options.Commands.UpdateOption
{
    public class UpdateOptionCommand : IRequest<UpdateOptionCommandResponse>
    {
        public  string? Text { get; set; }

        public bool? IsCorrect { get; set; }

        public Guid OptionId { get; set; }
    }
}
