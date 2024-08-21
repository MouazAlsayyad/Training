using AutoMapper;
using MediatR;
using Training.Application.Contracts.Persistence;
using Training.Application.Exceptions;
using Training.Application.Helper.Validators;
using Training.Domain.Entities;

namespace Training.Application.Features.Options.Commands.DeleteOption
{
    public class DeleteOptionCommandHandler(
        IQuestionRepository questionRepository,
        IOptionRepository optionRepository,
        IMapper mapper) : IRequestHandler<DeleteOptionCommand, DeleteOptionCommandResponse>
    {
        private readonly IOptionRepository _optionRepository = optionRepository ?? throw new ArgumentNullException(nameof(optionRepository));
        private readonly IQuestionRepository _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<DeleteOptionCommandResponse> Handle(DeleteOptionCommand request, CancellationToken cancellationToken)
        {
            var deleteOptionResponse = new DeleteOptionCommandResponse();

            var validationResponse = await RequestValidationHandler.ValidateAsync(
                request,
                new DeleteOptionCommandValidator(),
                () => deleteOptionResponse,
                cancellationToken
            );

            if (validationResponse is not null)
                return (DeleteOptionCommandResponse)validationResponse;

            // Retrieve the option to be deleted
            var option = await _optionRepository.GetByIdAsync(request.OptionId, cancellationToken) ??
                throw new NotFoundException(
                    nameof(Option), request.OptionId,
                    $"Option with ID {request.OptionId} was not found.");

            // Retrieve the related question
            var question = await _questionRepository.GetByIdAsync(option.QuestionId, cancellationToken) ??
                throw new NotFoundException(
                    nameof(Question), option.QuestionId,
                    $"Question with ID {option.QuestionId} was not found.");

            // Fetch all options related to the question
            var options = await _optionRepository.ListAllAsync(o => o.QuestionId == question.QuestionId, cancellationToken);

            // Check if the option to be deleted is the only correct option
            if (option.IsCorrect == true && options.Count(o => o.IsCorrect == true) == 1)
            {
                deleteOptionResponse.Success = false;
                deleteOptionResponse.Message = "Cannot delete this option because it is the only correct option for the question.";
                return deleteOptionResponse;
            }

            await _optionRepository.DeleteAsync(option, cancellationToken);

            deleteOptionResponse.Success = true;
            deleteOptionResponse.Message = "Option deleted successfully.";

            return deleteOptionResponse;
        }
    }
}
