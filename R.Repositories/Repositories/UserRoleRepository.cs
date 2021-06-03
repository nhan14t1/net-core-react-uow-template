using Microsoft.EntityFrameworkCore;
using R.Entities.Entities;
using R.Entities.Repositories;
using R.Repositories;

namespace R.Repositories.Repositories
{
    public class UserRoleRepository: Repository<AppUserRole>, IUserRoleRepository
    {
        public UserRoleRepository(DbContext context)
               : base(context)
        {

        }
    }
}
