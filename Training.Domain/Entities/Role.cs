using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Training.Domain.Common;

namespace Training.Domain.Entities
{
    public class Role : AuditableEntity
    {
        public int RoleId { get; set; }
        public string Name { get; set; } = string.Empty;

        public List<UserRole> UserRoles { get; set; } = null!;
    }
}
