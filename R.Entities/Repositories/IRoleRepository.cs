using System.Collections.Generic;
using System.Threading.Tasks;
using R.Entities.Entities;

namespace R.Entities.Repositories
{
    public interface IRoleRepository : IRepository<AppRole>
    {
        Task<List<AppRole>> GetByUserNameAsync(string userName);
        Task<AppRole> GetByNameAsync(string roleName);
    }
}
