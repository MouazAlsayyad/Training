using FluentValidation;
using Training.Application.Helper.Validators;
using Training.Domain.Entities;

namespace Training.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommandValidator : AbstractValidator<CreateQuestionCommand>
    {
        public CreateQuestionCommandValidator()
        {
            RuleFor(q => q.Text)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MaximumLength(500).WithMessage("{PropertyName} must not exceed 500 characters.");

            RuleFor(q => q.Options)
               .NotEmpty().WithMessage("Options are required.");
              //.Must(HaveAtLeastTwoOptions).WithMessage("At least two options are required.")
               //.Must(HaveAtLeastOneCorrectOption).WithMessage("At least one option must be correct.")
               //.Must(HaveUniqueOptionTexts).WithMessage("Option texts must be unique.")
               //.Must((command, options) => HaveValidCorrectOptions(command.AllowsMultipleCorrectAnswers, options))
               //.WithMessage("Only one correct option is allowed unless multiple correct answers are allowed.");

            RuleFor(q => q.Difficulty)
                .IsInEnum().WithMessage("Invalid difficulty level.");

            RuleFor(q => q.Mark)
                .GreaterThan(0).WithMessage("Mark must be greater than 0.");

            RuleFor(q => q.QuestionBankId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} cannot be null.")
                .Must(guid => GuidValidator.BeAValidGuid(guid)).WithMessage("{PropertyName} must be a valid GUID.");
        }

        private bool HaveAtLeastTwoOptions(ICollection<Option> options)
            => options.Count >= 2;

        private bool HaveAtLeastOneCorrectOption(ICollection<Option> options)
            => options.Any(o => (bool)o.IsCorrect);

        private bool HaveUniqueOptionTexts(ICollection<Option> options)
        {
            var optionTexts = options.Select(o => o.Text).ToList();
            return optionTexts.Distinct().Count() == optionTexts.Count;
        }

        private bool HaveValidCorrectOptions(bool allowsMultipleCorrectAnswers, ICollection<Option> options)
        {
            if (allowsMultipleCorrectAnswers)
                return true;

            return options.Count(o => (bool)o.IsCorrect) <= 1;
        }
    }
}
