using AutoMapper;
using MediatR;
using Training.Application.Contracts.Persistence;
using Training.Application.Exceptions;
using Training.Application.Features.Questions.Commands.CreateQuestion;
using Training.Application.Helper.Validators;
using Training.Domain.Entities;

namespace Training.Application.Features.Questions.Queries.GetQuestionsList
{
    public class GetQuestionsListQueryHandler(IQuestionRepository questionRepository, IMapper mapper) : IRequestHandler<GetQuestionsListQuery, GetQuestionsListQueryResponse>
    {
        private readonly IQuestionRepository _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<GetQuestionsListQueryResponse> Handle(GetQuestionsListQuery request, CancellationToken cancellationToken)
        {
            var response = new GetQuestionsListQueryResponse();

            var validationResponse = await RequestValidationHandler.ValidateAsync<GetQuestionsListQuery>(
                request,
                new GetQuestionListQueryValidator(),
                () => response,
                cancellationToken
            );

            if (validationResponse is not null)
                return (GetQuestionsListQueryResponse)validationResponse;

            IReadOnlyList<Question> questions =
             request.QuestionBankId.HasValue ?
             await _questionRepository.ListAllAsync
             (q => q.QuestionBankId == request.QuestionBankId.Value, cancellationToken) :

             await _questionRepository.ListAllAsync
             (q => q.ExamId == request.ExamId.Value, cancellationToken);

            if (questions.Count == 0)
                throw new NotFoundException
                    (nameof(Question), request.QuestionBankId ?? request.ExamId.Value);

            var questionDetailVmList = _mapper.Map<ICollection<QuestionListVm>>(questions);

            response.Questions = questionDetailVmList;
            response.Success = true;
            response.Message = "Questions retrieved successfully.";

            return response;
        }
    }
}
