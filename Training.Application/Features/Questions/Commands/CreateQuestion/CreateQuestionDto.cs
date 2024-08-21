using Training.Domain.Enums;

namespace Training.Application.Features.Questions.Commands.CreateQuestion
{
    public class CreateQuestionDto: QuestionDto
    {
        public ICollection<OptionDto> Options { get; set; } = [];
    }
}
