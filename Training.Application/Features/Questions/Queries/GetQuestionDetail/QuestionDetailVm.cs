using Training.Application.Features.Options.Queries.GetOptionById;
using Training.Application.Features.Questions.Commands.CreateQuestion;
using Training.Domain.Entities;
using Training.Domain.Enums;

namespace Training.Application.Features.Questions.Queries.GetQuestionDetail
{
    public class QuestionDetailVm
    {
        public Guid QuestionId { get; set; }
        public required string Text { get; set; } = string.Empty;

        public List<OptionDto> Options { get; set; } = [];

        public DifficultyLevel Difficulty { get; set; }
        public double Mark { get; set; }

    }
}
