using AutoMapper;
using MediatR;
using Training.Application.Contracts.Persistence;
using Training.Application.Exceptions;
using Training.Application.Helper.Validators;
using Training.Domain.Entities;

namespace Training.Application.Features.Options.Commands.UpdateOption
{
    public class UpdateOptionCommandHandler(
        IQuestionRepository questionRepository,
        IOptionRepository optionRepository,
        IMapper mapper) : IRequestHandler<UpdateOptionCommand, UpdateOptionCommandResponse>
    {
        private readonly IOptionRepository _optionRepository = optionRepository ?? throw new ArgumentNullException(nameof(optionRepository));
        private readonly IQuestionRepository _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<UpdateOptionCommandResponse> Handle(UpdateOptionCommand request, CancellationToken cancellationToken)
        {
            var updateOptionResponse = new UpdateOptionCommandResponse();

            var validationResponse = await RequestValidationHandler.ValidateAsync(
                request,
                new UpdateOptionCommandValidator(),
                () => updateOptionResponse,
                cancellationToken
            );

            if (validationResponse is not null)
                return (UpdateOptionCommandResponse)validationResponse;

            var option =
                await _optionRepository.GetByIdAsync(request.OptionId, cancellationToken) ??
                throw new NotFoundException(
                    nameof(Question), request.OptionId,
                    $"Option with ID {request.OptionId} was not found.");


            var question = await _questionRepository.GetByIdAsync(option.QuestionId, cancellationToken) ??
            throw new NotFoundException(
                nameof(Question), request.OptionId,
                $"Question with OptionID {request.OptionId} was not found.");


            // Fetch all options related to the question at once
            var options = await _optionRepository.ListAllAsync(o => o.QuestionId == question.QuestionId, cancellationToken);

            // Check and update the Text if provided and unique
            if (!string.IsNullOrEmpty(request.Text) && option.Text != request.Text)
            {
                if (options.Any(o => o.Text == request.Text))
                {
                    updateOptionResponse.Success = false;
                    updateOptionResponse.Message = "An option with the same text already exists for this question.";
                    return updateOptionResponse;
                }

                option.Text = request.Text;
            }

            // Check and update IsCorrect if provided
            if (request.IsCorrect.HasValue)
            {
                if (request.IsCorrect == true && !question.AllowsMultipleCorrectAnswers)
                {
                    if (options.Any(o => o.IsCorrect == true))
                    {
                        updateOptionResponse.Success = false;
                        updateOptionResponse.Message = "The question already has a correct option.";
                        return updateOptionResponse;
                    }
                }
                else if (request.IsCorrect == false && option.IsCorrect == true)
                {
                    var otherCorrectOptions = options
                        .Where(o => o.IsCorrect == true && o.OptionId != option.OptionId).ToList();

                    if (otherCorrectOptions.Count == 0)
                    {
                        updateOptionResponse.Success = false;
                        updateOptionResponse.Message = "Cannot set this option as incorrect because it is the only correct option for the question.";
                        return updateOptionResponse;
                    }
                }

                option.IsCorrect = request.IsCorrect.Value;
            }

            _mapper.Map(request, option);
            await _optionRepository.UpdateAsync(option, cancellationToken);

            updateOptionResponse.Success = true;
            updateOptionResponse.Message = "Option updated successfully.";
            updateOptionResponse.Option = _mapper.Map<UpdateOptionDto>(option);

            return updateOptionResponse;
        }
    }
}
