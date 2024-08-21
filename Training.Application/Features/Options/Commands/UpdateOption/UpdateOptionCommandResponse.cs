using Training.Application.Responses;

namespace Training.Application.Features.Options.Commands.UpdateOption
{
    public class UpdateOptionCommandResponse : BaseResponse
    {
        public UpdateOptionDto Option { get; set; } = default!;

        public UpdateOptionCommandResponse() : base()
        {
        }
    }
}
