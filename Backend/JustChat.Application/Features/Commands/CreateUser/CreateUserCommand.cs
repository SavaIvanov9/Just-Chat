using JustChat.Application.Interfaces;
using JustChat.Domain.Models.Users;

namespace JustChat.Application.Features.Commands.CreateUser
{
    public class CreateUserCommand : ICommand<User>
    {
        public string Name { get; set; }
    }
}
