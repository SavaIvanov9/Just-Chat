using System.Security.Claims;
using JustChat.Application.Exceptions;
using JustChat.Application.Features.Commands.ValidateToken;
using JustChat.Application.Features.Queries.GetUser;
using MediatR;
using Microsoft.IdentityModel.Tokens;

namespace JustChat.Api
{
    public class TokenValidator : ISecurityTokenValidator
    {
        private readonly IMediator _mediator;

        public TokenValidator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public bool CanValidateToken => true;

        public int MaximumTokenSizeInBytes { get; set; }

        public bool CanReadToken(string securityToken)
        {
            return string.IsNullOrWhiteSpace(securityToken) == false;
        }

        public ClaimsPrincipal ValidateToken(string securityToken, TokenValidationParameters validationParameters, out SecurityToken validatedToken)
        {
            try
            {
                var validateTokenCommand = new ValidateTokenCommand
                {
                    Value = securityToken
                };
                var token = _mediator.Send(validateTokenCommand).GetAwaiter().GetResult();

                var getUserQuery = new GetUserQuery
                {
                    Id = token.UserId
                };
                var user = _mediator.Send(getUserQuery).GetAwaiter().GetResult();

                var identity = new ClaimsIdentity("Custom");
                identity.AddClaim(new Claim(ClaimTypes.Name, user.Name));
                identity.AddClaim(new Claim("Id", user.Id.ToString()));

                var result = new ClaimsPrincipal(identity);
                validatedToken = new AuthenticationToken(token.Id.ToString(), token.ValidFrom, token.ValidTo);
                return result;
            }
            catch (AuthenticationApplicationException ex)
            {
            }

            validatedToken = null;
            return null;
        }
    }
}
