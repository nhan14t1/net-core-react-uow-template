using System;
using System.Collections.Generic;

#nullable disable

namespace R.Entities.Entities
{
    public partial class AppRole
    {
        public AppRole()
        {
            AppUserRoles = new HashSet<AppUserRole>();
        }

        public string Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<AppUserRole> AppUserRoles { get; set; }
    }
}
