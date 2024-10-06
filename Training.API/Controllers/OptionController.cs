using Azure;
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
        public async Task<ActionResult> CreateOption([FromBody] CreateOptionCommand createOptionCommand)
        {
            var response =  await _mediator.Send(createOptionCommand);
            
            return response.StatusCode switch
            {
                200 => Ok(response),
                404 => NotFound(response),
                _ => BadRequest(response)
            };
        }

        [HttpPut(Name = "UpdateOption")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateOption([FromBody] UpdateOptionCommand updateOptionCommand)
        {
            var response = await _mediator.Send(updateOptionCommand);

            return response.StatusCode switch
            {
                200 => Ok(response),
                404 => NotFound(response),
                _ => BadRequest(response)
            };
        }

        [HttpDelete("{optionId:guid}", Name = "DeleteOption")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteOption(Guid optionId)
        {
            var response = await _mediator.Send(new DeleteOptionCommand { OptionId = optionId });

            return response.StatusCode switch
            {
                200 => Ok(response),
                404 => NotFound(response),
                _ => BadRequest(response)
            };
        }

    }
}
