using System.Threading;
using System.Threading.Tasks;
using JustChat.Application.Constants;
using JustChat.Application.Exceptions;
using JustChat.Application.Interfaces;
using JustChat.Application.Interfaces.Services;
using JustChat.Application.Models.Specifications;
using JustChat.Domain.Models.Token;
using MediatR;

namespace JustChat.Application.Features.Commands.LogInUser
{
    public class LogInUserCommandHandler : IRequestHandler<LogInUserCommand, Token>
    {
        private readonly IDataUnitOfWork _data;
        private readonly ITokenService _tokenService;

        public LogInUserCommandHandler(
            IDataUnitOfWork data, 
            ITokenService tokenService)
        {
            _data = data;
            _tokenService = tokenService;
        }

        public async Task<Token> Handle(LogInUserCommand request, CancellationToken cancellationToken)
        {
            var spec = UserSpecification.GetByCredentials(request.UserName, request.Password);
            var user = await _data.Users.SingleOrDefaultAsync(spec);

            if(user == null)
            {
                throw new AuthenticationApplicationException(ErrorMessagesConstants.InvalidCredentials);
            }

            var token = await _tokenService.GenerateAsync(user);
            return token;
        }
    }
}
