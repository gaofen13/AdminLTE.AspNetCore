using AdminLTE.Domain.Entities;
using AdminLTE.Domain.IRepositories;

namespace AdminLTE.EntityFrameworkCore.Repositories
{
    public class MenuRepository : AdminLTERepositoryBase<Menu>, IMenuRepository
    {
        public MenuRepository(AdminDbContext dbcontext) : base(dbcontext)
        {

        }
    }
}
