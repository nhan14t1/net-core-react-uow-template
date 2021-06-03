using System;

namespace R.Entities.Exceptions
{
    public class AppException : Exception
    {
        public AppException(string message) : base(message)
        {
        }
    }
}
