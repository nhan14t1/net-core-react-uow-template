using System;

namespace R.Entities.Constants
{
    public class AppConst
    {
        public static string[] APP_ROLES = { "Admin", "User" };

        public static class AppRoles {
            public static string Admin = APP_ROLES[0];
            public static string User = APP_ROLES[1];
        }

        public const int COOKIE_DAYS = 30;

        public static readonly DateTime BASE_DATE = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Local);
        public static readonly DateTime BASE_DATE_UTC = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
    }
}
