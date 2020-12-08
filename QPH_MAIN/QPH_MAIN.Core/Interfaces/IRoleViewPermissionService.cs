using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.Entities;
using Sieve.Models;
using Sieve.Services;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IRoleViewPermissionService
    {
        ISieveProcessor SieveProcessor { get; set; }
        PagedList<RoleViewPermission> GetRoleViewPermissions (SieveModel sieveModel);
        Task InsertRoleViewPermission(RoleViewPermission roleViewPermission);
        Task<bool> DeleteRoleViewPermission(int id);        
    }
}
