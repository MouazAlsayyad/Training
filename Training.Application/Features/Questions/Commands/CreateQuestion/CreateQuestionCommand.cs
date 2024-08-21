using MediatR;
using Training.Domain.Entities;
using Training.Domain.Enums;

namespace Training.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionCommand : IRequest<CreateQuestionCommandResponse>
    {
        public string Text { get; set; } = string.Empty;
        public ICollection<Option> Options { get; set; } = [];
        public DifficultyLevel Difficulty { get; set; }
        public double Mark { get; set; }
        public Guid QuestionBankId { get; set; }
        public bool AllowsMultipleCorrectAnswers { get; set; }
    }
}
