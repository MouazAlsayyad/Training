using MediatR;
using Training.Application.Features.Options.Commands.CreateOption;
using Training.Application.Responses;
using Training.Domain.Entities;
using Training.Domain.Enums;

namespace Training.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommand : IRequest<BaseResponse<Guid>>
    {
        public string Text { get; set; } = string.Empty;
        public List<CreateOptionCommand>? Options { get; set; } 
        public DifficultyLevel Difficulty { get; set; }
        public double Mark { get; set; }
        public Guid QuestionBankId { get; set; }
        public bool AllowsMultipleCorrectAnswers { get; set; }
    }
}
