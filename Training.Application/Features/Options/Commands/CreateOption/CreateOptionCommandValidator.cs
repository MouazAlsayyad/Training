using FluentValidation;
using Training.Application.Helper.Validators;

namespace Training.Application.Features.Options.Commands.CreateOption
{
    public class CreateOptionCommandValidator : AbstractValidator<CreateOptionCommand>
    {
        public CreateOptionCommandValidator()
        {
            RuleFor(o => o.Text)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(o => o.IsCorrect)
                .NotNull().WithMessage("{PropertyName} is required.");

            RuleFor(q => q.QuestionId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} cannot be null.")
                .Must(guid => GuidValidator.BeAValidGuid(guid)).WithMessage("{PropertyName} must be a valid GUID.");
        }
    }
}
