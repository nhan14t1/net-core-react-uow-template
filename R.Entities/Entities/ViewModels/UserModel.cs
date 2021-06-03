using R.Entities.Entities;

namespace R.Entities.Entities.ViewModels
{
    public class UserModel : AppUser
    {
        public UserModel()
        {
        }

        public UserModel(AppUser input)
        {
            if (input != null)
            {
                Id = input.Id;
                FirstName = input.FirstName;
                LastName = input.LastName;
                AvatarUrl = input.AvatarUrl;
                UniquePath = input.UniquePath;
            }
        }
    }
}
