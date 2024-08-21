using FluentValidation;
using Training.Application.Helper.Validators;

namespace Training.Application.Features.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionCommandValidator : AbstractValidator<UpdateQuestionCommand>
    {
        public UpdateQuestionCommandValidator()
        {
            RuleFor(p => p.QuestionId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .Must(guid => GuidValidator.BeAValidGuid(guid)).WithMessage("{PropertyName} must be a valid GUID.");

            RuleFor(p => p.Text)
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.")
                .When(p => !string.IsNullOrEmpty(p.Text));

            RuleFor(p => p.Difficulty)
                .IsInEnum().When(p => p.Difficulty.HasValue).WithMessage("Invalid {PropertyName}.");

            RuleFor(p => p.Mark)
                .GreaterThan(0).When(p => p.Mark.HasValue).WithMessage("{PropertyName} must be greater than 0.");

            RuleFor(p => p.AllowsMultipleCorrectAnswers)
                .Must(value => value == true || value == false)
                .When(p => p.AllowsMultipleCorrectAnswers.HasValue)
                .WithMessage("{PropertyName} must be either true or false when provided.");

            RuleFor(p => p)
                .Must(HaveAtLeastOneValue)
                .WithMessage("At least one of {Text}, {AllowsMultipleCorrectAnswers}, {Mark}, or {Difficulty} must be provided.");
        }

        private bool HaveAtLeastOneValue(UpdateQuestionCommand command)
        {
            return !string.IsNullOrEmpty(command.Text) ||
                   command.AllowsMultipleCorrectAnswers.HasValue ||
                   command.Mark.HasValue ||
                   command.Difficulty.HasValue;
        }
    }
}
