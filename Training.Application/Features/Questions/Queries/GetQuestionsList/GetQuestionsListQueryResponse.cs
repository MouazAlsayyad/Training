using Training.Application.Responses;

namespace Training.Application.Features.Questions.Queries.GetQuestionsList
{
    public class GetQuestionsListQueryResponse : BaseResponse
    {
        public GetQuestionsListQueryResponse() : base() { }
        
        public ICollection<QuestionListVm>? Questions { get; set; }
    }
}
