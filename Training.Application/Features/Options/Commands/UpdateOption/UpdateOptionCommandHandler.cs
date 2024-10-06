using AutoMapper;
using MediatR;
using Training.Application.Contracts.Persistence;
using Training.Application.Exceptions;
using Training.Application.Helper.Validators;
using Training.Application.Responses;
using Training.Domain.Entities;

namespace Training.Application.Features.Options.Commands.UpdateOption
{
    public class UpdateOptionCommandHandler(
        IOptionRepository optionRepository,
        IQuestionRepository questionRepository,
        IMapper mapper) : IRequestHandler<UpdateOptionCommand, BaseResponse<object>>
    {
        private readonly IOptionRepository _optionRepository = optionRepository;
        private readonly IQuestionRepository _questionRepository = questionRepository;
        private readonly IMapper _mapper = mapper;

        public async Task<BaseResponse<object>> Handle(UpdateOptionCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateOptionCommandValidator();
            var validationResponse = await validator.ValidateAsync(request);

            if (validationResponse.Errors.Count() > 0)
            {
                return new BaseResponse<object>(
                    "There is Validation Errors",
                    false,
                    400,
                    validationResponse.Errors.Select(e => e.ErrorMessage).ToList());
            }
                

            var option = await _optionRepository.GetByIdAsync(request.OptionId);
            if(option == null)
            {
                return new BaseResponse<object>("Option not found", false, 404);
            }

            var question = await _questionRepository.GetByIdAsync(option.QuestionId);
            
            if(question == null)
            {
                _mapper.Map(request, option, typeof(UpdateOptionCommand), typeof(Option));
               
                await _optionRepository.UpdateAsync(option);

                return new BaseResponse<object>("Option has been Updated", true, 200);
            }


            // Fetch all options related to the question at once
            var options = await _optionRepository.ListAllAsync(o => o.QuestionId == question.QuestionId, cancellationToken);



            // Check and update IsCorrect if provided
            if (request.IsCorrect.HasValue)
            {
                if (request.IsCorrect == true && !question.AllowsMultipleCorrectAnswers)
                {
                    if (options.Any(o => o.IsCorrect == true))
                    {
                        return new BaseResponse<object>("The question already has a correct option", false, 404);
                    }
                }
                else if (request.IsCorrect == false && option.IsCorrect == true)
                {
                    var otherCorrectOptions = options
                        .Where(o => o.IsCorrect == true && o.OptionId != option.OptionId).ToList();

                    if (otherCorrectOptions.Count == 0)
                    {
                        return new BaseResponse<object>(
                            "Cannot set this option as incorrect because it is the only correct option for the question.",
                            false,
                            404);
                    }
                }

                option.IsCorrect = request.IsCorrect.Value;
            }

            _mapper.Map(request, option, typeof(UpdateOptionCommand), typeof(Option));

            await _optionRepository.UpdateAsync(option);

            return new BaseResponse<object>("Option has been Updated", true, 200);
        }
    }
}
