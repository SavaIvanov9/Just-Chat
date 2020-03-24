using System.Threading.Tasks;
using JustChat.Api.Interfaces;
using JustChat.Api.Models.User;
using JustChat.Api.TokenValidation;
using JustChat.Application.Features.Queries.GetUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JustChat.Api.Controllers
{
    [ServiceFilter(typeof(AuthGuardFilter))]
    public class TokenController : BaseController
    {
        private readonly ICurrentUser _currentUser;

        public TokenController(IMediator mediator, ICurrentUser currentUser)
            : base(mediator)
        {
            _currentUser = currentUser;
        }

        [HttpGet]
        public async Task<ActionResult> Validate()
        {
            var query = new GetUserQuery
            {
                Id = _currentUser.Id
            };

            var user = await Mediator.Send(query);

            var result = new GetUserResponse
            {
                UserId = user.Id,
                Username = user.Name
            };

            return this.Ok(result);
        }
    }
}
