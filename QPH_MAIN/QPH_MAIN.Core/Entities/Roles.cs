using Sieve.Attributes;
using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class Roles : BaseEntity
    {
        public Roles()
        {
            users = new HashSet<User>();
        }
        [Sieve(CanFilter = true, CanSort = true)]
        public string rolename { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public bool status { get; set; }
        public virtual ICollection<User> users { get; set; }
    }
}