using AutoMapper;
using MediatR;
using Training.Application.Contracts.Persistence;
using Training.Application.Exceptions;
using Training.Application.Features.Questions.Commands.CreateQuestion;
using Training.Application.Helper.Validators;
using Training.Domain.Entities;

namespace Training.Application.Features.Questions.Queries.GetQuestionDetail
{
    public class GetQuestionDetailQueryHandler(
        IOptionRepository optionRepository,
        IQuestionRepository questionRepository,
        IMapper mapper) : IRequestHandler<GetQuestionDetailQuery, GetQuestionDetailQueryResponse>
    {
        private readonly IOptionRepository _optionRepository = optionRepository ?? throw new ArgumentNullException(nameof(optionRepository));
        private readonly IQuestionRepository _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<GetQuestionDetailQueryResponse> Handle(GetQuestionDetailQuery request, CancellationToken cancellationToken)
        {
            var getQuestionDetailQueryResponse = new GetQuestionDetailQueryResponse();
            var validationResponse = await RequestValidationHandler.ValidateAsync(
                request,
                new GetQuestionDetailQueryValidator(),
                () => getQuestionDetailQueryResponse,
                cancellationToken
            );

            if (validationResponse is not null)
                return (GetQuestionDetailQueryResponse)validationResponse;

            var question =
                await _questionRepository.GetByIdAsync(request.QuestionId, cancellationToken) ??
               throw new NotFoundException(
                    nameof(Question), request.QuestionId,
                    $"Question with ID {request.QuestionId} was not found.");

            var questionDetailVm = _mapper.Map<QuestionDetailVm>(question);

            var options = await _optionRepository.ListAllAsync(o => o.QuestionId == request.QuestionId, cancellationToken);

            questionDetailVm.Options = _mapper.Map<ICollection<OptionDto>>(options);

            getQuestionDetailQueryResponse.QuestionDetail = questionDetailVm;
            getQuestionDetailQueryResponse.Success = true;
            getQuestionDetailQueryResponse.Message = "Question details retrieved successfully.";

            return getQuestionDetailQueryResponse;

        }
    }
}
