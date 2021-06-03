using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using R.Entities.Entities;
using R.Entities.Repositories;
using R.Repositories;

namespace R.Repositories.Repositories
{
    public class RoleRepository : Repository<AppRole>, IRoleRepository
    {
        public RoleRepository(DbContext context)
            : base(context)
        {

        }

        public async Task<List<AppRole>> GetByUserNameAsync(string userName)
        {
            userName = userName.ToLower();
            return await DbContext.Set<AppUser>().Where(_ => _.LowerUserName == userName)
                .Select(_ => _.AppUserRoles.Select(x => x.Role).ToList()).FirstOrDefaultAsync();
        }

        public async Task<AppRole> GetByNameAsync(string roleName)
        {
            roleName = roleName.ToLower();
            return await Entities.FirstOrDefaultAsync(_ => _.Name.ToLower() == roleName);
        }
    }
}
