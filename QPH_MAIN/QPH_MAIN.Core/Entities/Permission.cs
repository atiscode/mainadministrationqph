using Sieve.Attributes;
using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class Permission : BaseEntity
    {
        public Permission()
        {
            roleViewPermissions = new HashSet<RoleViewPermission>();
        }

        [Sieve(CanFilter = true, CanSort = true)]
        public string permission { get; set; }
        public bool? is_card { get; set; }
        public virtual ICollection<RoleViewPermission> roleViewPermissions { get; set; }
    }
}