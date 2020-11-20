using QPH_MAIN.Core.DTOs;
using QPH_MAIN.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface ISystemParametersRepository : ICodeRepository<SystemParameters>
    {
        IQueryable<SystemParameters> GetAllSystemParameters();
    }
}