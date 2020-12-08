using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class ViewsRepository : BaseRepository<View>, IViewRepository
    {
        public ViewsRepository(QPHContext context) : base(context) { }        
        public IQueryable<View> GetAllWithReferences()
        {
            return _entities.OrderByDescending(s => s.Id)
                .Include(t => t.roleViewPermissions)
                    .ThenInclude(u => u.role)
                .Include(t => t.roleViewPermissions)
                    .ThenInclude(u => u.permission);
        }
    }
}