using System;
using System.Threading.Tasks;
using R.Entities.Entities;
using R.Entities.Entities.ViewModels;

namespace R.Entities.Services
{
    public interface IUserService : IBaseService<AppUser>
    {
        Task<bool> IsValidUserCredentialsAsync(string userName, string password);

        Task<bool> IsAnExistingUserAsync(string userName);

        Task CreateUserAsync(string userName, string password, DateTime birthDate, string firstName, string lastName = null);

        Task<AppUser> GetByUserNameAsync(string userName);
        
        Task<UserModel> GetByUniqueNameAsync(string uniqueName);
        Task<UserModel> RegisterUser(UserModel user);
    }
}
