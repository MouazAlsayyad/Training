using MediatR;
using Training.Application.Features.Questions.Queries.GetQuestionDetail;
using Training.Application.Responses;

namespace Training.Application.Features.Questions.Queries.GetQuestionsList
{
    public class GetQuestionsListQuery : IRequest<BaseResponse<List<QuestionListVm>>>
    {
        public Guid? QuestionBankId { get; set; }
        public Guid? ExamId { get; set; }
    }
}
