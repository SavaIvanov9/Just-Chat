using System.Threading.Tasks;
using JustChat.Api.Models.Users;
using JustChat.Application.Commands.Users.Create;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace JustChat.Api.Controllers
{
    public class UserController : BaseController
    {
        public UserController(IMediator mediator)
            : base(mediator)
        {
        }

        [HttpPost]
        public async Task<ActionResult<CreateUserResponse>> Create([FromBody]CreateUserRequest request)
        {
            var command = new CreateUserCommand
            {
                Name = request.Name
            };

            var user = await Mediator.Send(command);

            var response = new CreateUserResponse
            {
                Id = user.Id
            };

            return this.Ok(response);
        }
    }
}
