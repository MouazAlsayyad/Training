using MediatR;

namespace Training.Application.Features.Questions.Queries.GetQuestionDetail
{
    public class GetQuestionDetailQuery: IRequest<GetQuestionDetailQueryResponse>
    {
        public Guid QuestionId { get; set; }
    }
}
