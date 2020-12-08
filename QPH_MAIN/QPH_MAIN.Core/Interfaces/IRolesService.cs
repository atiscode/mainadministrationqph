using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Core.CustomEntities;
using System.Threading.Tasks;
using Sieve.Models;
using Sieve.Services;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IRolesService
    {
        //PagedList<Roles> GetRoles(RolesQueryFilter filters);
        ISieveProcessor SieveProcessor { get; set; }
        PagedList<Role> GetRoles(SieveModel sieveModel);
        Task<Role> GetRole(int id);
        Task<Role> GetRoleByName(string name);
        Task InsertRole(Role role);
        Task<bool> UpdateRole(Role role);
        Task<bool> DeleteRole(int id);
    }
}