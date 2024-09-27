using System.ComponentModel.DataAnnotations.Schema;
using Training.Domain.Common;

namespace Training.Domain.Entities
{
    public class QuestionBank : AuditableEntity
    {
        public Guid QuestionBankId { get; set; }

        public string Name { get; set; } = string.Empty;

        public ICollection<Question> Questions { get; set; } = new List<Question>();

        [ForeignKey("Course")]
        public Guid CourseId { get; set; }

        // Navigation property for the one-to-one relationship
        public Course Course { get; set; } = null!;
    }
}
