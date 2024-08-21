using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Training.Domain.Common;

namespace Training.Domain.Entities
{
    public class Material : AuditableEntity
    {
        [Key]  
        public Guid MaterialId { get; set; }

        [Required]  
        [MaxLength(50)]  
        public required string Name { get; set; }

        [MaxLength(256)]  
        public string? Description { get; set; }

  
        public ICollection<Exam>? Exams { get; set; }

        [ForeignKey("MaterialId")]
        public QuestionBank? QuestionBank { get; set; }
    }
}
