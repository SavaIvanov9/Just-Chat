using JustChat.Application.Interfaces;
using JustChat.Domain.Models.Token;

namespace JustChat.Application.Features.Commands.LogInUser
{
    public class LogInUserCommand : ICommand<Token>
    {
        public string UserName { get; set; }

        public string Password { get; set; }
    }
}
