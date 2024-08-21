using Training.Application.Responses;

namespace Training.Application.Features.Questions.Queries.GetQuestionDetail
{
    public class GetQuestionDetailQueryResponse : BaseResponse
    {
        public GetQuestionDetailQueryResponse() : base(){}
        public QuestionDetailVm? QuestionDetail { get; set; }
    }
}
