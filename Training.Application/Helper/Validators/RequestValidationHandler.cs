using FluentValidation;
using Training.Application.Responses;

namespace Training.Application.Helper.Validators
{
    public static class RequestValidationHandler
    {
        public static async Task<BaseResponse?> ValidateAsync<TRequest>(
            TRequest request,
            IValidator<TRequest> validator,
            Func<BaseResponse> responseFactory,
            CancellationToken cancellationToken)
        {
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                var response = responseFactory();
                response.ValidationErrors = validationResult.Errors.Select(e => e.ErrorMessage).ToList();
                response.Success = false;
                response.Message = "Validation failed.";

                return response;
            }

            return null;
        }
    }
}