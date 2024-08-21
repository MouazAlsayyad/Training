using Training.Domain.Enums;

namespace Training.Application.Features.Questions.Commands
{
    public class QuestionDto
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; } = string.Empty;
        public DifficultyLevel Difficulty { get; set; }
        public double Mark { get; set; }
        public bool AllowsMultipleCorrectAnswers { get; set; }
    }
}
