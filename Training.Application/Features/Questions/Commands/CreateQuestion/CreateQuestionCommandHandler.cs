using AutoMapper;
using MediatR;
using Training.Application.Contracts.Persistence;
using Training.Domain.Entities;
using Training.Application.Exceptions;
using Training.Application.Helper.Validators;

namespace Training.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommandHandler(
        IQuestionRepository questionRepository,
        IQuestionBankRepository questionBankRepository,
        IOptionRepository optionRepository,
        IMapper mapper) : IRequestHandler<CreateQuestionCommand, CreateQuestionCommandResponse>
    {
        private readonly IQuestionBankRepository _questionBankRepository = questionBankRepository ?? throw new ArgumentNullException(nameof(questionBankRepository));
        private readonly IOptionRepository _optionRepository = optionRepository ?? throw new ArgumentNullException(nameof(optionRepository));
        private readonly IQuestionRepository _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<CreateQuestionCommandResponse> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
        {
            var createQuestionCommandResponse = new CreateQuestionCommandResponse();
            var validationResponse = await RequestValidationHandler.ValidateAsync(
                request,
                new CreateQuestionCommandValidator(),
                () => createQuestionCommandResponse,
                cancellationToken
            );

            if (validationResponse is not null)
                return (CreateQuestionCommandResponse)validationResponse;

            var questionBank =
                await _questionBankRepository.GetByIdAsync(request.QuestionBankId, cancellationToken) ??
                throw new NotFoundException(nameof(Question), request.QuestionBankId);
    
            var @question = _mapper.Map<Question>(request);

            @question = await _questionRepository.AddAsync(@question, cancellationToken);

            foreach (var option in request.Options)
            {
                option.QuestionId = question.QuestionId;
                await _optionRepository.AddAsync(option, cancellationToken);
            }
            var questionDto = _mapper.Map<CreateQuestionDto>(question);
            questionDto.Options = _mapper.Map<ICollection<OptionDto>>(request.Options);

            createQuestionCommandResponse.Success = true;
            createQuestionCommandResponse.Message = "Question created successfully.";
            createQuestionCommandResponse.Question = questionDto;

            return createQuestionCommandResponse;
        }
    }
}
