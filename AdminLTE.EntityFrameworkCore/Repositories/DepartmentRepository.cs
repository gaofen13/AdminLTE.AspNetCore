using AdminLTE.Domain.Entities;
using AdminLTE.Domain.IRepositories;

namespace AdminLTE.EntityFrameworkCore.Repositories
{
    public class DepartmentRepository : AdminLTERepositoryBase<Department>, IDepartmentRepository
    {
        public DepartmentRepository(AdminDbContext dbcontext) : base(dbcontext)
        {

        }
    }
}
