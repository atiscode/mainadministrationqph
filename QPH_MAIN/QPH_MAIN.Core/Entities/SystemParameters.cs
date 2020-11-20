using QPH_MAIN.Core.Entities;
using Sieve.Attributes;

namespace QPH_MAIN.Core.Entities
{
    public class SystemParameters : BaseEntityCode
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public string description { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string value { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string dataType { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public bool status { get; set; }
    }
}