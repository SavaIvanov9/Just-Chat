using System;

namespace JustChat.Domain.Exceptions
{
    public abstract class DomainException : Exception
    {
        protected DomainException(string message)
            : base(message)
        {
        }
    }
}