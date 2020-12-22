using QPH_MAIN.Core.CustomEntities;
using QPH_MAIN.Core.Entities;
using Sieve.Models;
using Sieve.Services;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IApplicationService
    {
        ISieveProcessor SieveProcessor { get; set; }
        PagedList<Application> GetApplications(SieveModel sieveModel);
    }
}
