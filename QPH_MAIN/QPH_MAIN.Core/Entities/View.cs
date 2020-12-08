using Sieve.Attributes;
using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class View : BaseEntity
    {
        public View()
        {
            roleViewPermissions = new HashSet<RoleViewPermission>();
        }

        [Sieve(CanFilter = true, CanSort = true)]
        public string code { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string name { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string route { get; set; }
        public int? parent { get; set; }
        public virtual ICollection<RoleViewPermission> roleViewPermissions { get; set; }
    }
}