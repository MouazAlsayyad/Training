using FluentValidation;
using Training.Application.Helper.Validators;

namespace Training.Application.Features.Options.Commands.UpdateOption
{
    public class UpdateOptionCommandValidator : AbstractValidator<UpdateOptionCommand>
    {
        public UpdateOptionCommandValidator()
        {
            RuleFor(o => o.Text)
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.")
                .When(o => !string.IsNullOrEmpty(o.Text));

            RuleFor(o => o.IsCorrect)
                .Must(isCorrect => isCorrect.HasValue)
                .When(o => o.IsCorrect.HasValue)
                .WithMessage("{PropertyName} must be true or false if provided.");

            RuleFor(q => q.OptionId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Must(guid => GuidValidator.BeAValidGuid(guid)).WithMessage("{PropertyName} must be a valid GUID.");

            // Custom rule to ensure that either Text or IsCorrect has a value
            RuleFor(o => o)
                .Must(HaveAtLeastOneValue)
                .WithMessage("At least one of {Text} or {IsCorrect} must have a value.");
        }
        private bool HaveAtLeastOneValue(UpdateOptionCommand command)
        {
            return !string.IsNullOrEmpty(command.Text) || command.IsCorrect.HasValue;
        }
    }
}
