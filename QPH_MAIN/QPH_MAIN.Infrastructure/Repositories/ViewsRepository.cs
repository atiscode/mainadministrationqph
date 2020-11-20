using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class ViewsRepository : BaseRepository<Views>, IViewRepository
    {
        public ViewsRepository(QPHContext context) : base(context) { }
        public async Task<Views> GetViewNameByHierarchyId(int viewId) => await _entities.FirstOrDefaultAsync(x => x.Id == viewId);

        public IQueryable<Views> GetAllViews() => _entities.OrderByDescending(s => s.Id).AsNoTracking();
    }
}