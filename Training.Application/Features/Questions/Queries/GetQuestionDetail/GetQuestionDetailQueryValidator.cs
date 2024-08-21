using FluentValidation;
using Training.Application.Helper.Validators;

namespace Training.Application.Features.Questions.Queries.GetQuestionDetail
{
    public class GetQuestionDetailQueryValidator : AbstractValidator<GetQuestionDetailQuery>
    {
        public GetQuestionDetailQueryValidator()
        {
            RuleFor(p => p.QuestionId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull().WithMessage("{PropertyName} cannot be null.")
                .Must(guid => GuidValidator.BeAValidGuid(guid)).WithMessage("{PropertyName} must be a valid GUID.");

        }
    }
}
