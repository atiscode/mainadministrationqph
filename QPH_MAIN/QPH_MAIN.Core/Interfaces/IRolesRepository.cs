﻿using QPH_MAIN.Core.Entities;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Core.Interfaces
{
    public interface IRolesRepository : IRepository<Roles>
    {
        Task<Roles> GetByName(string name);
        IQueryable<Roles> GetAllRoles();
    }
}