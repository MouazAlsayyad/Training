using FluentValidation;
using Training.Application.Helper.Validators;

namespace Training.Application.Features.Questions.Commands.DeleteQuestion
{
    public class DeleteQuestionCommandValidator : AbstractValidator<DeleteQuestionCommand>
    {
        public DeleteQuestionCommandValidator()
        {
            RuleFor(p => p.QuestionId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} cannot be null.")
                .Must(guid => GuidValidator.BeAValidGuid(guid)).WithMessage("{PropertyName} must be a valid GUID.");

        }
    }
}
