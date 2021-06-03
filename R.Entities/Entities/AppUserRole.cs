using System;
using System.Collections.Generic;

#nullable disable

namespace R.Entities.Entities
{
    public partial class AppUserRole
    {
        public string UserId { get; set; }
        public string RoleId { get; set; }

        public virtual AppRole Role { get; set; }
        public virtual AppUser User { get; set; }
    }
}
