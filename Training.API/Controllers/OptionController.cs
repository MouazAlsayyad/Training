using MediatR;
using Microsoft.AspNetCore.Mvc;
using Training.Application.Features.Options.Commands.CreateOption;
using Training.Application.Features.Options.Commands.DeleteOption;
using Training.Application.Features.Options.Commands.UpdateOption;

namespace Training.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OptionController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost(Name = "CreateOption")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CreateOptionCommandResponse>> CreateOption([FromBody] CreateOptionCommand createOptionCommand)
        {
            await _mediator.Send(createOptionCommand);
            return Ok();
        }

        [HttpPut(Name = "UpdateOption")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UpdateOptionCommandResponse>> UpdateOption([FromBody] UpdateOptionCommand updateOptionCommand)
        {
            var result = await _mediator.Send(updateOptionCommand);
            return Ok(result);
        }

        [HttpDelete("{optionId:guid}", Name = "DeleteOption")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOption(Guid optionId)
        {
            await _mediator.Send(new DeleteOptionCommand { OptionId = optionId });
            return NoContent();
        }

    }
}
