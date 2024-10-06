using MediatR;
using Training.Application.Responses;

namespace Training.Application.Features.Questions.Queries.GetQuestionDetail
{
    public class GetQuestionDetailQuery: IRequest<BaseResponse<QuestionDetailVm>>
    {
        public Guid QuestionId { get; set; }
    }
}
