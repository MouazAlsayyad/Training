using Training.Domain.Enums;

namespace Training.Application.Features.Questions.Queries.GetQuestionsList
{
    public class QuestionListVm
    {
        public Guid QuestionId { get; set; }
        public string Text { get; set; } = string.Empty;
        public DifficultyLevel Difficulty { get; set; }
        public double Mark { get; set; }
    }
}
