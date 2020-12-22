using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace QPH_MAIN.Core.Entities
{
    public partial class RoleViewPermission : BaseEntity
    {
        public int? id_role { get; set; }
        public int id_permission { get; set; }
        public int id_view { get; set; }

        [ForeignKey("id_role")]
        public virtual Role role { get; set; }

        [ForeignKey("id_permission")]
        public virtual Permission permission { get; set; }

        [ForeignKey("id_view")]
        public virtual View view { get; set; }
    }
}
