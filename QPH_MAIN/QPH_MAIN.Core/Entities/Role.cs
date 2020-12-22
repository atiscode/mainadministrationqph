using Sieve.Attributes;
using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class Role : BaseEntity
    {
        public Role()
        {
            userRoles = new HashSet<UserApplicationRole>();
            rolePermissions = new HashSet<RoleViewPermission>();
        }
        [Sieve(CanFilter = true, CanSort = true)]
        public string rolename { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public bool status { get; set; }
        public virtual ICollection<UserApplicationRole> userRoles { get; set; }
        public virtual ICollection<RoleViewPermission> rolePermissions { get; set; }
    }
}