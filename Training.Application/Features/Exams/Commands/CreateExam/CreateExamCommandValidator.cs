using FluentValidation;
using Training.Application.Helper.Validators;

namespace Training.Application.Features.Exams.Commands.CreateExam
{
    public class CreateExamCommandValidator : AbstractValidator<CreateExamCommand>
    {
        public CreateExamCommandValidator()
        {
            RuleFor(p => p.CourseId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} cannot be null.")
                .Must(guid => GuidValidator.BeAValidGuid(guid)).WithMessage("{PropertyName} must be a valid GUID.");

            RuleFor(p => p.Duration)
                .GreaterThan(TimeSpan.Zero).WithMessage("{PropertyName} must be greater than zero.")
                .LessThanOrEqualTo(TimeSpan.FromHours(3)).WithMessage("{PropertyName} cannot exceed 3 hours.");

            RuleFor(p => p.TimeOfExam)
                .GreaterThan(DateTime.UtcNow).WithMessage("{PropertyName} must be a future date.")
                .NotNull().WithMessage("{PropertyName} is required.");
        }
    }
}