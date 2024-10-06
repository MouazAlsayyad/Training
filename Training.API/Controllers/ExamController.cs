using MediatR;
using Microsoft.AspNetCore.Mvc;
using Training.Application.Features.Exams.Commands.CreateExam;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

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
        public async Task<ActionResult> CreateExam([FromBody] CreateExamCommand createExamCommand)
        {
            var response = await _mediator.Send(createExamCommand);
           
            return response.StatusCode switch
            {
                200 => Ok(response),
                404 => NotFound(response),
                _ => BadRequest(response)
            };
        }


        [HttpGet("{examId:guid}", Name = "GetExamById")]
        public async Task<IActionResult> GetExamById(Guid examId)
        {
            return Ok();
        }
    }
}
