using System.Threading;
using System.Threading.Tasks;
using JustChat.Application.Interfaces;
using JustChat.Application.Interfaces.Services;
using JustChat.Application.Models.Configurations;
using JustChat.Domain.Models.Token;
using JustChat.Domain.Models.Users;
using MediatR;

namespace JustChat.Application.Features.Commands.RegisterUser
{
    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Token>
    {
        private readonly IDataUnitOfWork _data;
        private readonly IHashingService _hashingService;
        private readonly ITokenService _tokenService;
        private readonly PasswordConfiguration _configuration;

        public RegisterUserCommandHandler(
            IDataUnitOfWork data, 
            IHashingService hashingService,
            ITokenService tokenService,
            PasswordConfiguration configuration)
        {
            _data = data;
            _hashingService = hashingService;
            _tokenService = tokenService;
            _configuration = configuration;
        }

        public async Task<Token> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _hashingService.GenerateHash(request.Password, _configuration.SaltString);
            var user = new User(request.UserName, passwordHash);

            await _data.Users.AddAsync(user);
            
            var token = await _tokenService.GenerateAsync(user);
            return token;
        }
    }
}
