using System;
using System.Threading;
using System.Threading.Tasks;
using JustChat.Application.Exceptions;
using JustChat.Application.Interfaces;
using JustChat.Application.Models.Configurations;
using JustChat.Application.Models.Specifications;
using JustChat.Domain.Models.Token;
using MediatR;

namespace JustChat.Application.Features.Commands.ValidateToken
{
    public class ValidateTokenCommandHandler : IRequestHandler<ValidateTokenCommand, Token>
    {
        private readonly IDataUnitOfWork _data;
        private readonly TokenConfiguration _configuration;

        public ValidateTokenCommandHandler(
            IDataUnitOfWork data, TokenConfiguration configuration)
        {
            _data = data;
            _configuration = configuration;
        }

        public async Task<Token> Handle(ValidateTokenCommand request, CancellationToken cancellationToken)
        {
            var spec = TokenSpecification.GetNotExpiredByValue(request.Value);
            var token = await _data.Tokens.SingleOrDefaultAsync(spec);

            if (token == null)
            {
                throw new AuthenticationApplicationException();
            }

            var now = DateTime.UtcNow;

            if(now < token.ValidTo && _configuration.ValidDurationInMinutes.Value > 0)
            {
                token.ResetExpiration(_configuration.ValidDuration);
            }

            return token;
        }
    }
}
