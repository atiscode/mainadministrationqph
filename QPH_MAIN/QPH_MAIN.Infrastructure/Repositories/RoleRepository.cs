using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class RoleRepository : BaseRepository<Role>, IRolesRepository
    {
        public RoleRepository(QPHContext context) : base(context) { }

        public async Task<Role> GetByName(string name) => await _entities.FirstOrDefaultAsync(x => x.rolename.ToLower() == name.ToLower());

        public IQueryable<Role> GetAllWithReferences()
        {
            return _entities.OrderByDescending(s => s.Id)
                .Include(t => t.rolePermissions)
                    .ThenInclude(u => u.role)
                .Include(t => t.rolePermissions)
                    .ThenInclude(u => u.permission)
                .Include(t => t.userRoles)
                    .ThenInclude(u => u.application)
                    .ThenInclude(u => u.catalogs);
        }
    }
}