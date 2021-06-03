using System.Collections.Generic;
using System.Threading.Tasks;
using R.Entities.Entities;

namespace R.Entities.Services
{
    public interface IRoleService : IBaseService<AppRole>
    {
        public Task<bool> IsInRoleAsync(string username, string role);
        
        public Task<List<AppRole>> GetByUserNameAsync(string username);

        public Task AddRolesAsync(string roleName);

        public Task AssignRoleToUser(string userName, string role);
    }
}
