using Sieve.Attributes;
using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class User : BaseEntity
    {
        public User()
        {
            userApplicationRoles = new HashSet<UserApplicationRole>();
        }

        public int id_country { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string nickname { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string firstName { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string lastName { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string email { get; set; }
        [Sieve(CanFilter = true, CanSort = true)]
        public string phone_number { get; set; }
        public string hashPassword { get; set; }
        public string google_access_token { get; set; }
        public string facebook_access_token { get; set; }
        public string firebase_token { get; set; }
        public bool is_account_activated { get; set; }
        public string profile_picture { get; set; }
        public bool status { get; set; }
        public string activation_code { get; set; }
        public virtual Country country { get; set; }
        public virtual ICollection<UserApplicationRole> userApplicationRoles { get; set; }
    }
}