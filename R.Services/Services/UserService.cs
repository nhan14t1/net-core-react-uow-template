using System;
using System.Threading.Tasks;
using R.Entities.Constants;
using R.Entities.Entities;
using R.Entities.Exceptions;
using R.Entities.Repositories;
using R.Entities.Services;
using R.Entities.UnitOfWorks;
using R.Entities.Utils;
using R.Repositories.Repositories;
using R.Entities.Entities.ViewModels;

namespace R.Services.Services
{
    public class UserService : BaseService<AppUser>, IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUserRoleRepository _userRoleRepository;

        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _userRepository = new UserRepository(unitOfWork.DbContext);
            _roleRepository = new RoleRepository(unitOfWork.DbContext);
            _userRoleRepository = new UserRoleRepository(unitOfWork.DbContext);
        }

        public async Task<bool> IsValidUserCredentialsAsync(string userName, string password)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password))
            {
                return false;
            }

            var user = await _userRepository.GetByUserNameAsync(userName);
            if (user == null)
            {
                return false;
            }

            return AppUtils.Sha256Hash(password) == user.Password;
        }

        public async Task<bool> IsAnExistingUserAsync(string userName)
        {
            return await _userRepository.GetByUserNameAsync(userName) != null;
        }

        public async Task CreateUserAsync(string userName, string password, DateTime birthDate, string firstName, string lastName = null)
        {
            if (string.IsNullOrWhiteSpace(userName) || string.IsNullOrWhiteSpace(password)
                || !AppUtils.IsEmail(userName))
            {
                throw new AppException("Invalid user");
            }

            var user = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = userName,
                Password = AppUtils.Sha256Hash(password),
                DateOfBirth = birthDate,
                FirstName = firstName,
                LastName = lastName,
                CreationDate = DateTime.UtcNow,
                IsActive = true,
                LowerUserName = userName.ToLower(),
                UniquePath = AppUtils.GenerateUniqueId().ToString()
            };

            await AddAsync(user);
        }

        public async Task<AppUser> GetByUserNameAsync(string userName)
        {
            if (string.IsNullOrWhiteSpace(userName))
            {
                return null;
            }

            return await _userRepository.GetByUserNameAsync(userName);
        }

        public async Task<UserModel> GetByUniqueNameAsync(string uniqueName)
        {
            var user = await _userRepository.GetByUniqueNameAsync(uniqueName);

            if (user == null) {
                throw new AppException("Invalid Id");
            }

            return new UserModel(user);
        }

        public async Task<UserModel> RegisterUser(UserModel user)
        {
            var newUser = new AppUser
            {
                Id = Guid.NewGuid().ToString(),
                UserName = user.UserName,
                LowerUserName = user.UserName.ToLower(),
                Password = AppUtils.Sha256Hash(user.Password),
                FirstName = user.FirstName,
                LastName = user.LastName,
                DateOfBirth = user.DateOfBirth,
                IsActive = true,
                EmailConfirmed = true,
                PhoneNumber = user.PhoneNumber,
                PhoneNumberConfirmed = true,
                CreationDate = DateTime.UtcNow,
                UniquePath = AppUtils.GenerateUniqueId().ToString()
            };

            var userRole = _roleRepository.GetByNameAsync(AppConst.AppRoles.User).Result;

            try
            {
                _unitOfWork.BeginTransaction().Wait();
                _userRepository.Add(newUser);
                _unitOfWork.SaveChanges();

                _userRoleRepository.Add(new AppUserRole {
                    UserId = newUser.Id,
                    RoleId = userRole.Id
                });

                await _unitOfWork.Commit();
            }
            catch (Exception e)
            {
                await _unitOfWork.RollBack();
                throw;
            }

            return new UserModel(newUser);
        }
    }
}
