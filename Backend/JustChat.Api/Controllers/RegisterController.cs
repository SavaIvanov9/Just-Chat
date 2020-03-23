using System.Threading.Tasks;
using JustChat.Api.Models.Users;
using JustChat.Application.Features.Commands.Users.Create;
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
        public async Task<ActionResult<CreateUserResponse>> Register([FromBody]CreateUserRequest request)
        {
            var command = new CreateUserCommand
            {
                Name = request.Name
            };

            var user = await Mediator.Send(command);

            var response = new CreateUserResponse
            {
                UserId = user.Id
            };

            return this.Ok(response);
        }
    }
}
