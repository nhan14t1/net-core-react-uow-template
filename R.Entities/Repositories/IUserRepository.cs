using System.Threading.Tasks;
using R.Entities.Entities;

namespace R.Entities.Repositories
{
    public interface IUserRepository : IRepository<AppUser>
    {
        Task<AppUser> GetByUserNameAsync(string userName);
        Task<AppUser> GetByUniqueNameAsync(string uniqueName);
    }
}
