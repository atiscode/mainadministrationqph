﻿using Microsoft.EntityFrameworkCore;
using QPH_MAIN.Core.DTOs;
using QPH_MAIN.Core.Entities;
using QPH_MAIN.Core.Interfaces;
using QPH_MAIN.Infrastructure.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QPH_MAIN.Infrastructure.Repositories
{
    public class SystemParametersRepository : BaseCodeRepository<SystemParameters>, ISystemParametersRepository
    {
        public SystemParametersRepository(QPHContext context) : base(context) { }

        public IQueryable<SystemParameters> GetAllSystemParameters() => _entities.AsNoTracking();
    }
}