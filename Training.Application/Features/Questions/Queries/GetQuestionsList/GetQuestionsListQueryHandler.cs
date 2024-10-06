using AutoMapper;
using MediatR;
using Training.Application.Contracts.Persistence;
using Training.Application.Exceptions;
using Training.Application.Features.Questions.Commands.CreateQuestion;
using Training.Application.Helper.Validators;
using Training.Application.Responses;
using Training.Domain.Entities;

namespace Training.Application.Features.Questions.Queries.GetQuestionsList
{
    public class GetQuestionsListQueryHandler(IQuestionRepository questionRepository, IMapper mapper) 
        : IRequestHandler<GetQuestionsListQuery, BaseResponse<List<QuestionListVm>>>
    {
        private readonly IQuestionRepository _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<BaseResponse<List<QuestionListVm>>> Handle(GetQuestionsListQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetQuestionListQueryValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count() > 0)
            {
                return new BaseResponse<List<QuestionListVm>>(
                    "There is validation Errors",
                    false,
                    400,
                    validationResult.Errors.Select(e=>e.ErrorMessage).ToList());
            }

            var questions = request.QuestionBankId.HasValue 
             ? await _questionRepository.ListAllAsync (q => q.QuestionBankId == request.QuestionBankId.Value)
             : await _questionRepository.ListAllAsync (q => q.ExamId == request.ExamId!.Value);

            var questionDetailVmList = _mapper.Map<List<QuestionListVm>>(questions);

            return new BaseResponse<List<QuestionListVm>>("", true, 200, questionDetailVmList);
        }
    }
}
