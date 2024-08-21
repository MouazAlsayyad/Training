using MediatR;
using Training.Application.Features.Questions.Queries.GetQuestionDetail;

namespace Training.Application.Features.Questions.Queries.GetQuestionsList
{
    public class GetQuestionsListQuery : IRequest<GetQuestionsListQueryResponse>
    {
        public Guid? QuestionBankId { get; set; }
        public Guid? ExamId { get; set; }
    }
}
