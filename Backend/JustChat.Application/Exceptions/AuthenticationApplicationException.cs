namespace JustChat.Application.Exceptions
{
    public class AuthenticationApplicationException : ApplicationException
    {
        public AuthenticationApplicationException(string message)
            : base(message)
        {
        }

        public AuthenticationApplicationException()
            : base()
        {
        }
    }
}
