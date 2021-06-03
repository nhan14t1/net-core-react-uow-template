using System;

namespace R.Entities.Entities.ViewModels
{
    public class LoginResult
    {
        // UserId
        public string Id { get; set; }

        public string UserName { get; set; }
        
        public string FirstName { get; set; }
        public string LastName { get; set; }
        
        public string AvatarUrl { get; set; }
        
        public string[] Roles { get; set; }

        public string AccessToken { get; set; }
        
        public long TokenExpirationInTimeStamp { get; set; }
    }
}
