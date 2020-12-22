using QPH_MAIN.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IPermissionsRepository : IRepository<Permission>
    {
        IQueryable<Permission> GetAllPermissions();
    }
}