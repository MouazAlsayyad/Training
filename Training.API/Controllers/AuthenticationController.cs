using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Training.Application.Features.Authentication.Login;

namespace Training.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthenticationController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("Login", Name = "Login")]
        public async Task<ActionResult<string>> Login([FromBody] LoginCommand user)
        {
            var response = await _mediator.Send(
                new LoginCommand()
                {
                    Email = user.Email,
                    Password = user.Password   
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
