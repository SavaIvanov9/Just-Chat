using System.Threading.Tasks;
using JustChat.Application.Interfaces;
using JustChat.Application.Interfaces.Services;
using JustChat.Application.Models.Configurations;
using JustChat.Domain.Interfaces;
using JustChat.Domain.Models.Token;
using JustChat.Domain.Models.Users;

namespace JustChat.Application.Services
{
    internal class TokenService : ITokenService
    {
        private readonly IDataUnitOfWork _data;
        private readonly IEncryptionService _encryptionService;
        private readonly TokenConfiguration _configuration;

        public TokenService(
            IDataUnitOfWork data,
            IEncryptionService encryptionService,
            TokenConfiguration configuration)
        {
            _data = data;
            _encryptionService = encryptionService;
            _configuration = configuration;
        }

        public async Task<Token> GenerateAsync(User user)
        {
            var token = new Token(user.Id, user.Name, user.Password, _configuration.ValidDuration);
            token.Encrypt(_encryptionService, _configuration.EncryptionKey);
            await _data.Tokens.AddAsync(token);

            return token;
        }
    }
}
