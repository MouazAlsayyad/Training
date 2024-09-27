using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Training.Domain.Common;

namespace Training.Domain.Entities
{
    public class Course : AuditableEntity
    {
        [Key]
        public Guid CourseId { get; set; }

        [Required]
        [MaxLength(50)]
        public required string Name { get; set; }

        [MaxLength(256)]
        public string? Description { get; set; }


        public ICollection<Exam>? Exams { get; set; }

        [ForeignKey("CourseId")]
        public QuestionBank? QuestionBank { get; set; }
    }
}
