using System.Linq;
using JustChat.Api.Interfaces;
using JustChat.Application.Features.Commands.ValidateToken;
using MediatR;
using Microsoft.AspNetCore.Mvc.Filters;

namespace JustChat.Api.TokenValidation
{
    public class AuthGuardFilter : IActionFilter
    {
        private readonly IMediator _mediator;
        private readonly ICurrentUser _currentUser;

        public AuthGuardFilter(IMediator mediator, ICurrentUser currentUser)
        {
            _mediator = mediator;
            _currentUser = currentUser;
        }

        public void OnActionExecuting(ActionExecutingContext actionContext)
        {
            var tokenValue = actionContext
                .HttpContext
                .Request
                .Headers
                .Where(x => x.Key == "Bearer")
                .Select(x => x.Value.FirstOrDefault())
                .FirstOrDefault();

            var command = new ValidateTokenCommand
            {
                Value = tokenValue
            };

            var token = _mediator.Send(command).GetAwaiter().GetResult();
            _currentUser.Id = token.UserId;
        }

        public void OnActionExecuted(ActionExecutedContext context)
        {
        }
    }
}
