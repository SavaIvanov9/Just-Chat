using System.Linq;
using JustChat.Application.Features.Commands.ValidateToken;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JustChat.Api.TokenValidation
{
    public class AuthGuardFilter : IActionFilter
    {
        private readonly IMediator _mediator;

        public AuthGuardFilter(
            IMediator mediator)
        {
            _mediator = mediator;
        }

        public void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var token = actionContext
                .HttpContext
                .Request
                .Headers
                .Where(x => x.Key == "Bearer")
                .Select(x => x.Value.FirstOrDefault())
                .FirstOrDefault();

            var command = new ValidateTokenCommand
            {
                Value = token
            };

            _mediator.Send(command).GetAwaiter().GetResult();
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
