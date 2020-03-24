using System.Threading.Tasks;
using JustChat.Api.Models.Users;
using JustChat.Application.Features.Commands.RegisterUser;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JustChat.Api.Controllers
{
    public class RegisterController : BaseController
    {
        public RegisterController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpPost]
        public async Task<ActionResult<RegisterUserResponse>> Register([FromBody]RegisterUserRequest request)
        {
            var command = new RegisterUserCommand
            {
                UserName = request.UserName,
                Password = request.Password
            };

            var token = await Mediator.Send(command);

            var response = new RegisterUserResponse
            {
                Token = token.Value,
                UserId = token.UserId
            };

            return this.Ok(response);
        }
    }
}
