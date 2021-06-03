using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using R.Entities.Entities;
using R.Entities.Exceptions;
using R.Entities.Repositories;
using R.Entities.Services;
using R.Entities.UnitOfWorks;
using R.Services;
using R.Repositories.Repositories;

namespace R.Services.Services
{
    public class RoleService : BaseService<AppRole>, IRoleService
    {
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUserRoleRepository _userRoleRepository;
        
        public RoleService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _roleRepository = new RoleRepository(unitOfWork.DbContext);
            _userRepository = new UserRepository(unitOfWork.DbContext);
            _userRoleRepository = new UserRoleRepository(unitOfWork.DbContext);
        }

        public async Task<bool> IsInRoleAsync(string userName, string role)
        {
            if (string.IsNullOrEmpty(userName))
            {
                return false;
            }

            role = role.ToLower();

            var roles = await _roleRepository.GetByUserNameAsync(userName);
            return roles != null && roles.Any(_ => _.Name.ToLower() == role);
        }

        public async Task<List<AppRole>> GetByUserNameAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username))
            {
                return null;
            }

            return await _roleRepository.GetByUserNameAsync(username);
        }

        public async Task AddRolesAsync(string roleName)
        {
            await _roleRepository.AddAsync(new AppRole
            {
                Id = Guid.NewGuid().ToString(),
                Name = roleName
            });
        }

        public async Task AssignRoleToUser(string userName, string roleName)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(roleName))
            {
                throw new AppException("User or role is not found");
            }

            var user = await _userRepository.GetByUserNameAsync(userName);

            if (user == null)
            {
                throw new AppException("User is not found");
            }

            var role = await _roleRepository.GetByNameAsync(roleName);

            await _userRoleRepository.AddAsync(new AppUserRole {
                UserId = user.Id,
                RoleId = role.Id
            });
        }
    }
}
