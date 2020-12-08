using System;
using System.Collections.Generic;
using System.Text;

namespace QPH_MAIN.Core.Entities
{
    public partial class Application: BaseEntity
    {
        public Application()
        {
            userApplicationRoles = new HashSet<UserApplicationRole>();
            catalogs = new HashSet<ApplicationCatalog>();
        }
        public string name { get; set; }
        public virtual ICollection<UserApplicationRole> userApplicationRoles { get; set; }
        public virtual ICollection<ApplicationCatalog> catalogs{ get; set; }
    }
}
