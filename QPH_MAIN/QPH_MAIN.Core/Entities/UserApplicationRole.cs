using System.Collections.Generic;

namespace QPH_MAIN.Core.Entities
{
    public partial class UserApplicationRole : BaseEntity
    {
        public int id_user { get; set; }
        public int id_application { get; set; }
        public int id_role { get; set; }
        public int id_enterprise { get; set; }

        public virtual User user { get; set; }
        public virtual Application application { get; set; }
        public virtual Role role { get; set; }
        public virtual Enterprise enterprise { get; set; }
        
    }
}
