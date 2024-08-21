using MediatR;

namespace Training.Application.Features.Options.Commands.DeleteOption
{
    public class DeleteOptionCommand : IRequest<DeleteOptionCommandResponse>
    {
        public Guid OptionId { get; set; }

    }
}
