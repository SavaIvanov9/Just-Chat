using System.Threading.Tasks;
using JustChat.Api.Models.Login;
using JustChat.Application.Features.Commands.LogInUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JustChat.Api.Controllers
{
    public class LoginController : BaseController
    {
        public LoginController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpPost]
        public async Task<ActionResult<LogInResponse>> LogIn([FromBody]LogInRequest request)
        {
            var command = new LogInUserCommand
            {
                UserName = request.UserName,
                Password = request.Password
            };

            var token = await Mediator.Send(command);

            var response = new LogInResponse
            {
                Token = token.Value,
                UserId = token.UserId
            };

            return this.Ok(response);
        }
    }
}
