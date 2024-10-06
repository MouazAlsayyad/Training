using AutoMapper;
using MediatR;
using Training.Application.Contracts.Persistence;
using Training.Application.Exceptions;
using Training.Application.Features.Exams.Commands.CreateExam;
using Training.Application.Features.Questions.Commands.CreateQuestion;
using Training.Application.Helper.Validators;
using Training.Application.Responses;
using Training.Domain.Entities;

namespace Training.Application.Features.Options.Commands.CreateOption
{
    public class CreateOptionCommandHandler(
        IQuestionRepository questionRepository,
        IOptionRepository optionRepository,
        IMapper mapper) : IRequestHandler<CreateOptionCommand, BaseResponse<Guid>>
    {
        private readonly IOptionRepository _optionRepository = optionRepository ?? throw new ArgumentNullException(nameof(optionRepository));
        private readonly IQuestionRepository _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<BaseResponse<Guid>> Handle(CreateOptionCommand request, CancellationToken cancellationToken)
        {
            var Question = await _questionRepository.GetByIdAsync(request.QuestionId);
            
            if (Question == null)
            {
                return new BaseResponse<Guid>("Question Not Found", false, 400);
            }

            var validator = new CreateOptionCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (validationResult.Errors.Count() > 0)
            {
                return new BaseResponse<Guid>("There is Validation Errors", false, 400, validationResult.Errors.Select(e => e.ErrorMessage).ToList());
            }
            else
            {
                var Option = _mapper.Map<Option>(request);

                var data = await _optionRepository.AddAsync(Option);

                return new BaseResponse<Guid>("", true, 200, data.OptionId);
            }

        }

    }
}
