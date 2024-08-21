using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Training.Domain.Common;

namespace Training.Domain.Entities
{
    public class Option : AuditableEntity
    {
        [Key]
        public Guid OptionId { get; set; }

        [Required]
        [MaxLength(500)]  // Assuming the option text should not exceed 500 characters
        public string Text { get; set; } = string.Empty;

        public bool IsCorrect { get; set; }

        [Required]
        public Guid QuestionId { get; set; }

        // Navigation property for the many-to-one relationship with Question
        [ForeignKey("QuestionId")]
        public Question Question { get; set; }
    }
}
