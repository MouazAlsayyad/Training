using AutoMapper;
using MediatR;
using Training.Application.Contracts.Persistence;
using Training.Application.Exceptions;
using Training.Application.Helper.Validators;
using Training.Domain.Entities;

namespace Training.Application.Features.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionCommandHandler(
        IQuestionRepository questionRepository,
        IOptionRepository optionRepository,
        IMapper mapper) : IRequestHandler<UpdateQuestionCommand, UpdateQuestionCommandResponse>
    {
        private readonly IOptionRepository _optionRepository = optionRepository ?? throw new ArgumentNullException(nameof(optionRepository));
        private readonly IQuestionRepository _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<UpdateQuestionCommandResponse> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var updateQuestionCommandResponse = new UpdateQuestionCommandResponse();
            var validationResponse = await RequestValidationHandler.ValidateAsync(
                request,
                new UpdateQuestionCommandValidator(),
                () => updateQuestionCommandResponse,
                cancellationToken
            );

            if (validationResponse is not null)
                return (UpdateQuestionCommandResponse)validationResponse;

            var questionToUpdate =
                await _questionRepository.GetByIdAsync(request.QuestionId, cancellationToken) ??
                throw new NotFoundException(
                    nameof(Question), request.QuestionId,
                    $"Question with ID {request.QuestionId} was not found.");


            if (request.AllowsMultipleCorrectAnswers.HasValue &&
                request.AllowsMultipleCorrectAnswers.Value != questionToUpdate.AllowsMultipleCorrectAnswers)
            {
                var correctOptions =
                    await _optionRepository.ListAllAsync(
                        o => o.QuestionId == request.QuestionId && o.IsCorrect == true,
                        cancellationToken);

                if (!request.AllowsMultipleCorrectAnswers.Value && correctOptions.Count > 1)
                {
                    updateQuestionCommandResponse.Success = false;
                    updateQuestionCommandResponse.Message = "Cannot set AllowsMultipleCorrectAnswers to false when multiple correct options exist.";
                    return updateQuestionCommandResponse;
                }

            }

            _mapper.Map(request, questionToUpdate);

            await _questionRepository.UpdateAsync(questionToUpdate, cancellationToken);
            updateQuestionCommandResponse.Success = true;
            updateQuestionCommandResponse.Message = "Question updated successfully.";
            updateQuestionCommandResponse.Question = _mapper.Map<UpdateQuestionDto>(questionToUpdate);

            return updateQuestionCommandResponse;
        }
    }
}
