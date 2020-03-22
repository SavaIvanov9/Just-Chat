using System.Collections.Generic;
using FluentValidation.Results;

namespace JustChat.Application.Exceptions
{
    public class ValidationApplicationException : ApplicationException
    {
        public ValidationApplicationException()
            : base("One or more validation failures have occurred.")
        {
        }

        public ValidationApplicationException(IReadOnlyCollection<ValidationFailure> failures)
            : this()
        {
            Failures = failures;
        }

        public IReadOnlyCollection<ValidationFailure> Failures { get; }
    }
}
