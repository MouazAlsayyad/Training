using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Domain.Common;

namespace Training.Domain.Entities
{
    public class UserRole : AuditableEntity
    {
        public int Id { get; set; }
        public User User { get; set; } = null!;
        
        [ForeignKey(nameof(User))]
        public int UserId {  get; set; }
        
        public Role Role { get; set; } = null!;
        
        [ForeignKey(nameof(Role))]
        public int RoleId { get; set; }
    }
}
