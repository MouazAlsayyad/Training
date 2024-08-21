using AutoMapper;
using MediatR;
using Training.Application.Contracts.Persistence;
using Training.Application.Exceptions;
using Training.Application.Helper.Validators;
using Training.Domain.Entities;

namespace Training.Application.Features.Questions.Commands.DeleteQuestion
{
    public class DeleteQuestionCommandHandler(
        IQuestionRepository questionRepository,
        IOptionRepository optionRepository,
        IMapper mapper) : IRequestHandler<DeleteQuestionCommand, DeleteQuestionCommandResponse>
    {
        private readonly IOptionRepository _optionRepository = optionRepository ?? throw new ArgumentNullException(nameof(optionRepository));
        private readonly IQuestionRepository _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<DeleteQuestionCommandResponse> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
            var deleteQuestionCommandResponse = new DeleteQuestionCommandResponse();
            var validationResponse = await RequestValidationHandler.ValidateAsync(
                request,
                new DeleteQuestionCommandValidator(),
                () => deleteQuestionCommandResponse,
                cancellationToken
            );

            if (validationResponse is not null)
                return (DeleteQuestionCommandResponse)validationResponse;

            var questionToDelete =
                await _questionRepository.GetByIdAsync(request.QuestionId, cancellationToken) ??
                throw new NotFoundException(
                    nameof(Question), request.QuestionId,
                    $"Question with ID {request.QuestionId} was not found.");

            await _optionRepository.DeleteAllOptionsAsync(o => o.QuestionId == request.QuestionId, cancellationToken);

            await _questionRepository.DeleteAsync(questionToDelete, cancellationToken);

            deleteQuestionCommandResponse.Success = true;
            deleteQuestionCommandResponse.Message = "Question deleted successfully.";

            return deleteQuestionCommandResponse;
        }
    }
}
