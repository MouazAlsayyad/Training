using AutoMapper;
using MediatR;
using Training.Application.Contracts.Persistence;
using Training.Application.Exceptions;
using Training.Application.Helper.Validators;
using Training.Application.Responses;
using Training.Domain.Entities;

namespace Training.Application.Features.Questions.Commands.DeleteQuestion
{
    public class DeleteQuestionCommandHandler(
        IQuestionRepository questionRepository,
        IOptionRepository optionRepository,
        IMapper mapper) : IRequestHandler<DeleteQuestionCommand, BaseResponse<object>>
    {
        private readonly IOptionRepository _optionRepository = optionRepository ?? throw new ArgumentNullException(nameof(optionRepository));
        private readonly IQuestionRepository _questionRepository = questionRepository ?? throw new ArgumentNullException(nameof(questionRepository));
        private readonly IMapper _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        public async Task<BaseResponse<object>> Handle(DeleteQuestionCommand request, CancellationToken cancellationToken)
        {
           
           

            var questionToDelete = await _questionRepository.GetByIdAsync(request.QuestionId);
            if(questionToDelete == null)
            {
                return new BaseResponse<object>("Question not found", false, 404);
            }

            await _questionRepository.DeleteAsync(questionToDelete, cancellationToken);
            return new BaseResponse<object>("question has been Deleted", true, 200);
        }
    }
}
