using System.Threading.Tasks;
using JustChat.Domain.Models.Token;
using JustChat.Domain.Models.Users;

namespace JustChat.Application.Interfaces.Services
{
    public interface ITokenService
    {
        Task<Token> GenerateAsync(User user);
    }
}
