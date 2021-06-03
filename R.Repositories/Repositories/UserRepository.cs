using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using R.Entities.Entities;
using R.Entities.Repositories;
using R.Repositories;

namespace R.Repositories.Repositories
{
    public class UserRepository : Repository<AppUser>, IUserRepository
    {
        public UserRepository(DbContext context)
            : base(context)
        {

        }

        public async Task<AppUser> GetByUserNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return null;
            }

            userName = userName.ToLower();
            return await Entities.FirstOrDefaultAsync(_ => _.LowerUserName == userName);
        }

        public async Task<AppUser> GetByUniqueNameAsync(string uniqueName)
        {
            if (string.IsNullOrWhiteSpace(uniqueName))
            {
                return null;
            }

            uniqueName = uniqueName.ToLower();
            return await Entities.FirstOrDefaultAsync(_ => _.UniquePath == uniqueName);
        }
    }
}
