using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Linq;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class PermissionsRepository : BaseRepository<Permission>, IPermissionsRepository
    {
        public PermissionsRepository(QPHContext context) : base(context) { }
        public IQueryable<Permission> GetAllWithReferences()
        {
            return _entities
                .Include(p => p.roleViewPermissions)
                    .ThenInclude(r => r.role)
                .Include(p => p.roleViewPermissions)
                    .ThenInclude(r => r.permission);
        }
        public IQueryable<Permission> GetAllPermissions() => _entities.OrderByDescending(s => s.Id).AsNoTracking();
    }
}