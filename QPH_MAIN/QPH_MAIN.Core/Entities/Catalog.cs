using Sieve.Attributes;
using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class Catalog : BaseEntity
    {
        public Catalog()
        {
            applicationCatalogs = new HashSet<ApplicationCatalog>();
        }
        [Sieve(CanFilter = true, CanSort = true)]
        public string code { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string name { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string description { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public bool status { get; set; }
        public virtual ICollection<ApplicationCatalog> applicationCatalogs { get; set; }
    }
}