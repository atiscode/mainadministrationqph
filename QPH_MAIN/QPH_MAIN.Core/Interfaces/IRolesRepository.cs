using QPH_MAIN.Core.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IRolesRepository : IRepository<Role>
    {
        Task<Role> GetByName(string name);
        IQueryable<Role> GetAllWithReferences();
    }
}