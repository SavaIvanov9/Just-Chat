using JustChat.Application.Interfaces;
using JustChat.Domain.Models.Token;

namespace JustChat.Application.Features.Commands.RegisterUser
{
    public class RegisterUserCommand : ICommand<Token>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
