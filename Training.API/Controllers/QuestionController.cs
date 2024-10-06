using Azure;
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

        [HttpGet("GetAllQuestions", Name = "GetAllQuestions")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetAllQuestions([FromQuery] GetQuestionsListQuery getQuestionsListQuery)
        {
            var response = await _mediator.Send(getQuestionsListQuery);

            return response.StatusCode switch
            {
                200 => Ok(response),
                404 => NotFound(response),
                _ => BadRequest(response)
            };
        }

        [HttpGet("{questionId:guid}", Name = "GetQuestionDetail")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> GetQuestionDetail(Guid questionId)
        {
            var response = await _mediator.Send(
                new GetQuestionDetailQuery { QuestionId = questionId });

            return response.StatusCode switch
            {
                200 => Ok(response),
                404 => NotFound(response),
                _ => BadRequest(response)
            };
        }

        [HttpPost(Name = "CreateQuestion")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult> CreateQuestion([FromBody] CreateQuestionCommand createQuestionCommand)
        {
            var response = await _mediator.Send(createQuestionCommand);


            return response.StatusCode switch
            {
                200 => Ok(response),
                404 => NotFound(response),
                _ => BadRequest(response)
            };
        }

        [HttpPut(Name = "UpdateQuestion")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> UpdateQuestion([FromBody] UpdateQuestionCommand updateQuestionCommand)
        {
            var response = await _mediator.Send(updateQuestionCommand);

            return response.StatusCode switch
            {
                200 => Ok(response),
                404 => NotFound(response),
                _ => BadRequest(response)
            };
        }


        [HttpDelete("{questionId:guid}", Name = "DeleteQuestion")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteQuestion(Guid questionId)
        {
           var response =  await _mediator.Send(new DeleteQuestionCommand()
           { 
               QuestionId = questionId 
           });
            
            return response.StatusCode switch
            {
                200 => Ok(response),
                404 => NotFound(response),
                _ => BadRequest(response)
            };
        }
    }
}
