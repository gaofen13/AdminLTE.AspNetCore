using AdminLTE.Domain.Entities;
using AdminLTE.Domain.IRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminLTE.EntityFrameworkCore.Repositories
{
    public class DepartmentRepository : FonourRepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AdminDbContext dbcontext) : base(dbcontext)
        {

        }
    }
}
