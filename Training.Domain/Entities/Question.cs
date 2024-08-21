using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Training.Domain.Common;
using Training.Domain.Enums;

namespace Training.Domain.Entities
{
    public class Question : AuditableEntity
    {
        [Key]
        public Guid QuestionId { get; set; }

        [Required]
        [MaxLength(1000)]  // Assuming the text of the question should not exceed 1000 characters
        public string Text { get; set; } = string.Empty;

        // Navigation property for the one-to-many relationship with Option
        public ICollection<Option> Options { get; set; } = new List<Option>();

        public DifficultyLevel Difficulty { get; set; }

        [Range(0, double.MaxValue)]  // Mark must be greater than or equal to 0
        public double Mark { get; set; }

        public Guid? ExamId { get; set; }

        // Navigation property for the many-to-one relationship with Exam
        [ForeignKey("ExamId")]
        public Exam? Exam { get; set; }

        [Required]
        public Guid QuestionBankId { get; set; }

        // Navigation property for the many-to-one relationship with QuestionBank
        [ForeignKey("QuestionBankId")]
        public required QuestionBank QuestionBank { get; set; }

        public bool AllowsMultipleCorrectAnswers { get; set; }
    }
}
