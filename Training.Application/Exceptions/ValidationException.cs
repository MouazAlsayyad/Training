using FluentValidation.Results;

namespace Training.Application.Exceptions
{
    public class ValidationException : Exception
    {
        public IReadOnlyList<string> ValidationErrors { get; }

        public ValidationException(ValidationResult validationResult)
        {
            ValidationErrors = validationResult.Errors
                .Select(error => error.ErrorMessage)
                .ToList()
                .AsReadOnly();
        }

        public ValidationException(string message)
            : base(message)
        {
            ValidationErrors = new List<string>().AsReadOnly();
        }

        public ValidationException(string message, IEnumerable<string> errors)
            : base(message)
        {
            ValidationErrors = errors.ToList().AsReadOnly();
        }

        public override string ToString()
        {
            var errorMessages = string.Join("; ", ValidationErrors);
            return string.IsNullOrEmpty(errorMessages)
                ? base.ToString()
                : $"{base.ToString()}, Validation Errors: {errorMessages}";
        }
    }
}
