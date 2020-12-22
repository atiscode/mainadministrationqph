using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Linq;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class RoleViewPermissionRepository : BaseRepository<RoleViewPermission>, IRoleViewPermissionRepository
    {
        public RoleViewPermissionRepository(QPHContext context) : base(context) { }
       
        public IQueryable<RoleViewPermission> GetAllWithReferences()
        {
            return _entities
                .Include(p => p.permission)
                .Include(v => v.view)
                .Include(r => r.role);
        }
    }
}
