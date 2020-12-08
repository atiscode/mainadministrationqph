using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class ApplicationRepository : BaseRepository<Application>, IApplicationRepository
    {
        public ApplicationRepository(QPHContext context) : base(context) { }

        public IQueryable<Application> GetAllWithReferences()
        {
            return _entities
                .Include(catalogs => catalogs.catalogs)
                    .ThenInclude(c => c.catalog);
        }
    }
}
