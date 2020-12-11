using Sieve.Attributes;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace QPH_MAIN.Core.Entities
{
    public partial class PermissionStatus
    {
        [Sieve(CanFilter = true, CanSort = true)]
        public int id { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string permission { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        //public int statuses { get; set; }
        //public bool status { get {
        //        return (statuses == 1) ? true : false;
        //    }
        //}
        public bool status { get; set; }
    }
}