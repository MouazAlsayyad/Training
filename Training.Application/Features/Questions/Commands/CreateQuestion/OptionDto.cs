namespace Training.Application.Features.Questions.Commands.CreateQuestion
{
    public class OptionDto
    {
        public Guid OptionId { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
    }
}
