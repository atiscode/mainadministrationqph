using QPH_MAIN.Core.Entities;
using System.Linq;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IRoleViewPermissionRepository : IRepository<RoleViewPermission>
    {
        IQueryable<RoleViewPermission> GetAllWithReferences();
    }
}
