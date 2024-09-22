using System.Net;
using System.Text.Json;
using Training.Application.Exceptions;

namespace Training.API.Middleware
{
    public class ExceptionHandlingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = exception switch
            {
                NotFoundException => HttpStatusCode.NotFound,
                FluentValidation.ValidationException => HttpStatusCode.BadRequest,
                _ => HttpStatusCode.InternalServerError
            };

            var response = exception switch
            {
                NotFoundException => new { exception.Message, Detail = string.Empty, Errors = (List<string>?)null },
                FluentValidation.ValidationException validationException => new
                {
                    validationException.Message,
                    Detail = string.Empty,
                    Errors = validationException.Errors.Select(e => e.ErrorMessage).ToList()
                },
                _ => new { Message = "An unexpected error occurred.", Detail = exception.Message, Errors = (List<string>?)null }
            };

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)statusCode;

            var jsonResponse = JsonSerializer.Serialize(response);
            return context.Response.WriteAsync(jsonResponse);
        }



    }
}
