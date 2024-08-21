using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Training.Domain.Common;

namespace Training.Domain.Entities
{
    public class Exam : AuditableEntity
    {
        [Key]
        public Guid ExamId { get; set; }

        // Navigation property for the one-to-many relationship with Question
        public ICollection<Question> Questions { get; set; } = [];

        public TimeSpan Duration { get; set; } = TimeSpan.FromHours(1).Add(TimeSpan.FromMinutes(15));
        
        [Required]
        public DateTime TimeOfExam { get; set; }

        [Required]
        public Guid MaterialId { get; set; }

        // Navigation property for the many-to-one relationship with Material
        [ForeignKey("MaterialId")]
        public required Material Material { get; set; }
    }
}
