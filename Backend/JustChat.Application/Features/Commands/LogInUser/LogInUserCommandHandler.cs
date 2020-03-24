using System.Threading;
using System.Threading.Tasks;
using JustChat.Application.Constants;
using JustChat.Application.Exceptions;
using JustChat.Application.Interfaces;
using JustChat.Application.Interfaces.Services;
using JustChat.Application.Models.Configurations;
using JustChat.Application.Models.Specifications;
using JustChat.Domain.Models.Token;
using MediatR;

namespace JustChat.Application.Features.Commands.LogInUser
{
    public class LogInUserCommandHandler : IRequestHandler<LogInUserCommand, Token>
    {
        private readonly IDataUnitOfWork _data;
        private readonly IHashingService _hashingService;
        private readonly ITokenService _tokenService;
        private readonly PasswordConfiguration _configuration;

        public LogInUserCommandHandler(
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

        public async Task<Token> Handle(LogInUserCommand request, CancellationToken cancellationToken)
        {
            var passwordHash = _hashingService.GenerateHash(request.Password, _configuration.SaltString);
            var spec = UserSpecification.GetByCredentials(request.UserName, passwordHash);
            var user = await _data.Users.SingleOrDefaultAsync(spec);

            if (user == null)
            {
                throw new AuthenticationApplicationException(ErrorMessagesConstants.InvalidCredentials);
            }

            var token = await _tokenService.GenerateAsync(user);
            return token;
        }
    }
}
