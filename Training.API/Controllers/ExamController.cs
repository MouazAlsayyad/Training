using MediatR;
using Microsoft.AspNetCore.Mvc;
using Training.Application.Features.Exams.Commands.CreateExam;

namespace Training.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExamController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpPost(Name = "CreateExam")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CreateExamCommandResponse>> CreateExam([FromBody] CreateExamCommand createExamCommand)
        {
            var result = await _mediator.Send(createExamCommand);
            return CreatedAtAction(nameof(GetExamById), new { examId = result.ExamId }, result); // Return 201 Created
        }


        [HttpGet("{examId:guid}", Name = "GetExamById")]
        public async Task<IActionResult> GetExamById(Guid examId)
        {
            return Ok();
        }
    }
}
