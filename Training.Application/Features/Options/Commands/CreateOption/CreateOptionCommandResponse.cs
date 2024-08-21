using Training.Application.Responses;

namespace Training.Application.Features.Options.Commands.CreateOption
{
    public class CreateOptionCommandResponse: BaseResponse
    {
        public CreateOptionCommandResponse() : base()
        {

        }
        public CreateOptionDto Option{ get; set; } = default!;
    }
}