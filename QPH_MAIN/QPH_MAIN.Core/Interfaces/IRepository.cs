using QPH_MAIN.Core.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IRepository<T> where T : BaseEntity
    {        
        IQueryable<T> GetAll();
        Task<T> GetById(int id);
        Task Add(T entity);
        void Update(T entity);
        void Update(T oldEntity, T entity);
        Task Delete(int id);
    }
}