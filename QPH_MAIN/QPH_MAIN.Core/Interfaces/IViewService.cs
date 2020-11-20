using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.QueryFilters;
using QPH_MAIN.Core.CustomEntities;
using System.Threading.Tasks;
using Sieve.Models;
using Sieve.Services;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IViewService
    {
        ISieveProcessor SieveProcessor { get; set; }
        PagedList<Views> GetViews(SieveModel sieveModel);
        Task<Views> GetView(int id);
        Task InsertView(Views views);
        Task<bool> RebuildHierarchy(Tree tree, int idUser);
        Task<bool> UpdateView(Views views);
        Task<bool> DeleteView(int id);
        Task DeleteHierarchyByUserId(int userId);
    }
}