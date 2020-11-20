using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Core.CustomEntities;
using System.Threading.Tasks;
using Sieve.Models;
using Sieve.Services;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IPermissionsService
    {
        ISieveProcessor SieveProcessor { get; set; }
        PagedList<Permissions> GetPermissions(SieveModel sieveModel);
        Task<Permissions> GetPermission(int id);
        Task InsertPermission(Permissions permissions);
        Task<bool> UpdatePermission(Permissions permissions);
        Task<bool> DeletePermission(int id);
    }
}