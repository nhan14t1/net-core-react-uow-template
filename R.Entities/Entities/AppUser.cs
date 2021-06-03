using System;
using System.Collections.Generic;

#nullable disable

namespace R.Entities.Entities
{
    public partial class AppUser
    {
        public AppUser()
        {
            AppUserRoles = new HashSet<AppUserRole>();
        }

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public bool IsActive { get; set; }
        public string AvatarUrl { get; set; }
        public string UserName { get; set; }
        public bool EmailConfirmed { get; set; }
        public string Password { get; set; }
        public string PhoneNumber { get; set; }
        public bool PhoneNumberConfirmed { get; set; }
        public DateTime? CreationDate { get; set; }
        public string UniquePath { get; set; }
        public string LowerUserName { get; set; }

        public virtual ICollection<AppUserRole> AppUserRoles { get; set; }
    }
}
