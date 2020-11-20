using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Linq;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class PermissionsRepository : BaseRepository<Permissions>, IPermissionsRepository
    {
        public PermissionsRepository(QPHContext context) : base(context) { }

        public IQueryable<Permissions> GetAllPermissions() => _entities.OrderByDescending(s => s.Id).AsNoTracking();
    }
}