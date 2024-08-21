namespace Training.Application.Features.Options.Commands.UpdateOption
{
    public class UpdateOptionDto
    {
        public Guid OptionId { get; set; }
        public string Text { get; set; } = string.Empty;
        public bool IsCorrect { get; set; }
        public Guid QuestionId { get; set; }
    }
}
