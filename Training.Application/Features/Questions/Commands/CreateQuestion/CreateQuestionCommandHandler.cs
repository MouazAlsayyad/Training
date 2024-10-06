using AutoMapper;
using MediatR;
using Training.Application.Contracts.Persistence;
using Training.Domain.Entities;
using Training.Application.Exceptions;
using Training.Application.Helper.Validators;
using Training.Application.Responses;
using Training.Application.Features.Options.Commands.CreateOption;

namespace Training.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommandHandler(
        IQuestionRepository questionRepository,
        IQuestionBankRepository questionBankRepository,
        IOptionRepository optionRepository,
        IMapper mapper) : IRequestHandler<CreateQuestionCommand, BaseResponse<Guid>>
    {
        private readonly IQuestionBankRepository _questionBankRepository = questionBankRepository ?? throw new ArgumentNullException(nameof(questionBankRepository));
        private readonly IOptionRepository _optionRepository = optionRepository ?? throw new ArgumentNullException(nameof(optionRepository));
        private readonly IQuestionRepository _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<BaseResponse<Guid>> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateQuestionCommandValidator();
            var validationResponse = await validator.ValidateAsync(request);

            if (validationResponse.Errors.Count() > 0)
            {
                return new BaseResponse<Guid>("There is Validator Errors", 
                    false,
                    400,
                    validationResponse.Errors.Select(e=>e.ErrorMessage).ToList());
            }
                

            var questionBank = await _questionBankRepository.GetByIdAsync(request.QuestionBankId);
    
            if(questionBank == null)
            {
                return new BaseResponse<Guid>("Question Bank not found", false, 404);
            }
            var @question = _mapper.Map<Question>(request);

            @question = await _questionRepository.AddAsync(@question, cancellationToken);

            if(request.Options != null)
            {
                foreach (var option in request.Options!)
                {
                    option.QuestionId = question.QuestionId;
                    var OptionToAdd = _mapper.Map<Option>(option);
                    await _optionRepository.AddAsync(OptionToAdd);
                }
                
            }
           

            return new BaseResponse<Guid>("Question has been Created", true, 200, question.QuestionId);
        }
    }
}
