using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Core.CustomEntities;
using System.Threading.Tasks;
using QPH_MAIN.Core.DTOs;
using Sieve.Models;
using Sieve.Services;

namespace QPH_MAIN.Core.Interfaces
{
    public interface ISystemParametersService
    {
        ISieveProcessor SieveProcessor { get; set; }
        PagedList<SystemParameters> GetSystemParameters(SieveModel sieveModel);
        Task<SystemParameters> GetSystemParameters(string code);
        Task InsertSystemParameters(SystemParameters systemParameters);
        Task<bool> UpdateSystemParameters(SystemParameters systemParameters);
        Task<bool> DeleteSystemParameters(string code);
    }
}