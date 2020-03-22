using JustChat.Application.Interfaces;
using JustChat.Domain.Models.Users;

namespace JustChat.Application.Commands.Users.Create
{
    public class CreateUserCommand : ICommand<User>
    {
        public string Name { get; set; }
    }
}
