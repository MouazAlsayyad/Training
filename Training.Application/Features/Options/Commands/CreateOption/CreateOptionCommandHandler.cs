using AutoMapper;
using MediatR;
using Training.Application.Contracts.Persistence;
using Training.Application.Exceptions;
using Training.Application.Helper.Validators;
using Training.Domain.Entities;

namespace Training.Application.Features.Options.Commands.CreateOption
{
    public class CreateOptionCommandHandler(
        IQuestionRepository questionRepository,
        IOptionRepository optionRepository,
        IMapper mapper) : IRequestHandler<CreateOptionCommand, CreateOptionCommandResponse>
    {
        private readonly IOptionRepository _optionRepository = optionRepository ?? throw new ArgumentNullException(nameof(optionRepository));
        private readonly IQuestionRepository _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<CreateOptionCommandResponse> Handle(CreateOptionCommand request, CancellationToken cancellationToken)
        {

            var createOptionCommandResponse = new CreateOptionCommandResponse();
            var validationResponse = await RequestValidationHandler.ValidateAsync(
                request,
                new CreateOptionCommandValidator(),
                () => createOptionCommandResponse,
                cancellationToken
            );

            if (validationResponse is not null )
                return (CreateOptionCommandResponse)validationResponse;



            var question = await _questionRepository.GetByIdAsync(request.QuestionId, cancellationToken) ??
                throw new NotFoundException(
                    nameof(Question), request.QuestionId,
                    $"Question with ID {request.QuestionId} was not found.");

            var existingOptions = await _optionRepository
                .ListAllAsync(o => o.QuestionId == request.QuestionId && o.Text == request.Text, cancellationToken);

            if (existingOptions.Any())
            {
                createOptionCommandResponse.Success = false;
                createOptionCommandResponse.Message = "An option with the same text already exists for this question.";
                return createOptionCommandResponse;
            }

            if (request.IsCorrect.HasValue && request.IsCorrect.Value)
            {
                var correctOptionExists = await _optionRepository
                    .ListAllAsync(
                    o => o.QuestionId == request.QuestionId 
                    && o.IsCorrect == true, cancellationToken);

                if (!question.AllowsMultipleCorrectAnswers && correctOptionExists.Any())
                {
                    createOptionCommandResponse.Success = false;
                    createOptionCommandResponse.Message = "The question already has a correct option.";
                    return createOptionCommandResponse;
                }
            }
            var option = _mapper.Map<Option>(request);
            option = await _optionRepository.AddAsync(option, cancellationToken);

            createOptionCommandResponse.Success = true;
            createOptionCommandResponse.Message = "Option created successfully.";
            createOptionCommandResponse.Option = _mapper.Map<CreateOptionDto>(option);

            return createOptionCommandResponse;
        }

    }
}
