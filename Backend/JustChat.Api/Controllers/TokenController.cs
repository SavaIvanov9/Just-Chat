using System.Threading.Tasks;
using JustChat.Api.Models.Tokens;
using JustChat.Api.TokenValidation;
using JustChat.Application.Features.Commands.ValidateToken;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JustChat.Api.Controllers
{
    [ServiceFilter(typeof(AuthGuardFilter))]
    public class TokenController : BaseController
    {
        public TokenController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpPost]
        public async Task<ActionResult> Validate([FromBody]ValidateTokenRequest request)
        {
            var command = new ValidateTokenCommand
            {
                Value = request.Token
            };

            await Mediator.Send(command);

            return this.Ok();
        }
    }
}
