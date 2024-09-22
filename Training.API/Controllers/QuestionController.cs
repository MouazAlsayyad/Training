using MediatR;
using Microsoft.AspNetCore.Mvc;
using Training.Application.Features.Questions.Commands.CreateQuestion;
using Training.Application.Features.Questions.Commands.DeleteQuestion;
using Training.Application.Features.Questions.Commands.UpdateQuestion;
using Training.Application.Features.Questions.Queries.GetQuestionDetail;
using Training.Application.Features.Questions.Queries.GetQuestionsList;

namespace Training.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator;

        [HttpGet("all", Name = "GetAllQuestions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetQuestionsListQueryResponse>> GetAllQuestions([FromQuery] GetQuestionsListQuery getQuestionsListQuery)
        {
            var result = await _mediator.Send(getQuestionsListQuery);
            return Ok(result);
        }

        [HttpGet("{questionId:guid}", Name = "GetQuestionDetail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<GetQuestionDetailQueryResponse>> GetQuestionDetail(Guid questionId)
        {
            var result = await _mediator.Send(
                new GetQuestionDetailQuery { QuestionId = questionId });
            return Ok(result);
        }

        [HttpPost(Name = "CreateQuestion")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<CreateQuestionCommandResponse>> CreateQuestion([FromBody] CreateQuestionCommand createQuestionCommand)
        {
            var result = await _mediator.Send(createQuestionCommand);

            return CreatedAtAction(
                nameof(GetQuestionDetail),
                new { questionId = result.Question.QuestionId }, result);
        }

        [HttpPut(Name = "UpdateQuestion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<UpdateQuestionCommandResponse>> UpdateQuestion([FromBody] UpdateQuestionCommand updateQuestionCommand)
        {
            var result = await _mediator.Send(updateQuestionCommand);
            return Ok(result);
        }


        [HttpDelete("{questionId:guid}", Name = "DeleteQuestion")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteQuestion(Guid questionId)
        {
            await _mediator.Send(
                new DeleteQuestionCommand { QuestionId = questionId });
            return NoContent();
        }
    }
}
