using AutoMapper;
using MediatR;
using Training.Application.Contracts.Persistence;
using Training.Application.Exceptions;
using Training.Application.Helper.Validators;
using Training.Application.Responses;
using Training.Domain.Entities;

namespace Training.Application.Features.Questions.Commands.UpdateQuestion
{
    public class UpdateQuestionCommandHandler(
        IQuestionRepository questionRepository,
        IOptionRepository optionRepository,
        IMapper mapper) : IRequestHandler<UpdateQuestionCommand, BaseResponse<object>>
    {
        private readonly IOptionRepository _optionRepository = optionRepository ?? throw new ArgumentNullException(nameof(optionRepository));
        private readonly IQuestionRepository _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<BaseResponse<object>> Handle(UpdateQuestionCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateQuestionCommandValidator();
            var validationResponse = await validator.ValidateAsync(request, cancellationToken);

            if (validationResponse.Errors.Count() > 0)
            {
                return new BaseResponse<object>("There is Validation Errors", false, 400);
            }
                

            var questionToUpdate = await _questionRepository.GetByIdAsync(request.QuestionId);
            
            if(questionToUpdate == null)
            {
                return new BaseResponse<object>("Question not found", false, 404);
            }

            if (request.AllowsMultipleCorrectAnswers.HasValue &&
                request.AllowsMultipleCorrectAnswers.Value != questionToUpdate.AllowsMultipleCorrectAnswers)
            {
                var correctOptions =
                    await _optionRepository.ListAllAsync(
                        o => o.QuestionId == request.QuestionId && o.IsCorrect == true,
                        cancellationToken);

                if (!request.AllowsMultipleCorrectAnswers.Value && correctOptions.Count > 1)
                {
                    return new BaseResponse<object>("Cannot set AllowsMultipleCorrectAnswers to false when multiple correct options exist.", false, 400);
                }

            }

            _mapper.Map(request, questionToUpdate, typeof(UpdateQuestionCommand), typeof(Question));

            await _questionRepository.UpdateAsync(questionToUpdate, cancellationToken);
            
            return new BaseResponse<object>("Question has been Updated", true, 200);
        }
    }
}
