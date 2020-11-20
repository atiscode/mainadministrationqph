using Sieve.Attributes;
using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class Views : BaseEntity
    {
        public Views()
        {
            userView = new HashSet<UserView>();
            viewCard = new HashSet<ViewCard>();
        }
        [Sieve(CanFilter = true, CanSort = true)]
        public string code { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string name { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string route { get; set; }
        public virtual ICollection<UserView> userView { get; set; }
        public virtual ICollection<ViewCard> viewCard { get; set; }
    }
}