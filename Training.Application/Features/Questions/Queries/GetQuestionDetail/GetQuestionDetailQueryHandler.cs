using AutoMapper;
using MediatR;
using Training.Application.Contracts.Persistence;
using Training.Application.Exceptions;
using Training.Application.Features.Options.Queries.GetOptionById;
using Training.Application.Features.Questions.Commands.CreateQuestion;
using Training.Application.Helper.Validators;
using Training.Application.Responses;
using Training.Domain.Entities;

namespace Training.Application.Features.Questions.Queries.GetQuestionDetail
{
    public class GetQuestionDetailQueryHandler(
        IOptionRepository optionRepository,
        IQuestionRepository questionRepository,
        IMapper mapper) : IRequestHandler<GetQuestionDetailQuery, BaseResponse<QuestionDetailVm>>
    {
        private readonly IOptionRepository _optionRepository = optionRepository ?? throw new ArgumentNullException(nameof(optionRepository));
        private readonly IQuestionRepository _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<BaseResponse<QuestionDetailVm>> Handle(GetQuestionDetailQuery request, CancellationToken cancellationToken)
        {

            var question = await _questionRepository.GetByIdAsync(request.QuestionId);

            if(question == null)
            {
                return new BaseResponse<QuestionDetailVm>("Question not Found", false, 404);
            }
            var questionDetailVm = _mapper.Map<QuestionDetailVm>(question);

            var options = await _optionRepository.ListAllAsync(o => o.QuestionId == request.QuestionId);

            questionDetailVm.Options = _mapper.Map<List<OptionDto>>(options);

            return new BaseResponse<QuestionDetailVm>("", true, 200, questionDetailVm);

        }
    }
}
