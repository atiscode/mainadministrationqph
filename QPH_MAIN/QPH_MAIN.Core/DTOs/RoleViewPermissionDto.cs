using System;
using System.Collections.Generic;
using System.Text;

namespace QPH_MAIN.Core.DTOs
{
    public class RoleViewPermissionDto
    {
        public int? id_role { get; set; }
        public int id_permission { get; set; }
        public int id_view { get; set; }
        public string rolename { get; set; }
        public string view { get; set; }
        public string permission { get; set; }
    }
}
