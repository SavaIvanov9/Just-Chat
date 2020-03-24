using JustChat.Application.Interfaces;
using JustChat.Domain.Models.Token;

namespace JustChat.Application.Features.Commands.ValidateToken
{
    public class ValidateTokenCommand : ICommand<Token>
    {
        public string Value { get; set; }
    }
}
