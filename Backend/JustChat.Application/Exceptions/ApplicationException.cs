using System;

namespace JustChat.Application.Exceptions
{
    public abstract class ApplicationException : Exception
    {
        protected ApplicationException(string message)
            : base(message)
        {
        }

        public ApplicationException()
            : base()
        {
        }
    }
}