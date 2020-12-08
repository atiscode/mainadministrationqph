using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Core.CustomEntities;
using System.Threading.Tasks;
using Sieve.Models;
using Sieve.Services;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IPermissionService
    {
        ISieveProcessor SieveProcessor { get; set; }
        PagedList<Permission> GetPermissions(SieveModel sieveModel);
        Task<Permission> GetPermission(int id);
        Task InsertPermission(Permission permissions);
        Task<bool> UpdatePermission(Permission permissions);
        Task<bool> DeletePermission(int id);
    }
}