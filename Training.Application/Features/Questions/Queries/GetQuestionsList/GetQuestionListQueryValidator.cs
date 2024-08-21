using FluentValidation;
using Training.Application.Features.Questions.Queries.GetQuestionsList;
using Training.Application.Helper.Validators;

namespace Training.Application.Features.Questions.Commands.CreateQuestion
{
    public class GetQuestionListQueryValidator : AbstractValidator<GetQuestionsListQuery>
    {
        public GetQuestionListQueryValidator()
        {
            RuleFor(q => q.QuestionBankId)
                .Must(guid => guid == null || GuidValidator.BeAValidGuid(guid)).WithMessage("{PropertyName} must be a valid GUID.");

            RuleFor(q => q.ExamId)
                .Must(guid => guid == null || GuidValidator.BeAValidGuid(guid)).WithMessage("{PropertyName} must be a valid GUID.");

            RuleFor(q => q)
                .Custom((query, context) =>
                {
                    if ((query.QuestionBankId == null && query.ExamId == null) ||
                        (query.QuestionBankId != null && query.ExamId != null))
                    {
                        context.AddFailure("Exactly one of {QuestionBankId} or {ExamId} must be provided.");
                    }
                });
        }
    }
}
