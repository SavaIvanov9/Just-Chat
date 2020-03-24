using JustChat.Api.TokenValidation;
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

        [HttpGet]
        public ActionResult Validate()
        {
            return this.Ok();
        }
    }
}
