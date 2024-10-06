using MediatR;
using Training.Application.Responses;

namespace Training.Application.Features.Options.Commands.UpdateOption
{
    public class UpdateOptionCommand : IRequest<BaseResponse<object>>
    {
        public  string? Text { get; set; }

        public bool? IsCorrect { get; set; }

        public Guid OptionId { get; set; }
    }
}
