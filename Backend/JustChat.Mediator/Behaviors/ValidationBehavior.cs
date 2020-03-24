using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using JustChat.Application.Exceptions;
using MediatR;

namespace JustChat.Mediator.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
      where TRequest : IRequest<TResponse>
    {
        private readonly IEnumerable<IValidator<TRequest>> _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(
            TRequest request, 
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            var validationContext = new ValidationContext(request);

            var failures = (await Task.WhenAll(_validators
                .Select(v => v.ValidateAsync(validationContext, cancellationToken))))
                .SelectMany(result => result.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Count != 0)
            {
                throw new ValidationApplicationException(failures);
            }

            return await next();
        }
    }
}
