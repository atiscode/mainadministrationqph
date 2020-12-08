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
        PagedList<View> GetViews(SieveModel sieveModel);
        Task<View> GetView(int id);
        Task InsertView(View views);
        Task<bool> RebuildHierarchy(Tree tree, int idUser);
        Task<bool> UpdateView(View views);
        Task<bool> DeleteView(int id);
        Task DeleteHierarchyByUserId(int userId);
    }
}