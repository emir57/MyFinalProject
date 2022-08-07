using System;

namespace Core.Exceptions.UnAuthorized
{
    public class UnAuthorizedException : Exception
    {
        public UnAuthorizedException() : base("Erişim Engellendi")
        {
        }

        public UnAuthorizedException(string message) : base(message)
        {
        }

        public UnAuthorizedException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
