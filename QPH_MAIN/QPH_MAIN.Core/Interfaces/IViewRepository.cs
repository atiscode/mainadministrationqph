using QPH_MAIN.Core.Entities;
using System.Linq;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IViewRepository : IRepository<View>
    {
        IQueryable<View> GetAllWithReferences();
    }
}