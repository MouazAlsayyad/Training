using MediatR;
using Training.Application.Responses;

namespace Training.Application.Features.Options.Commands.DeleteOption
{
    public class DeleteOptionCommand : IRequest<BaseResponse<object>>
    {
        public Guid OptionId { get; set; }

    }
}
