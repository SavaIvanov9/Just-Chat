using System.Threading;
using System.Threading.Tasks;
using JustChat.Application.Exceptions;
using JustChat.Application.Interfaces;
using JustChat.Application.Models.Specifications;
using JustChat.Domain.Models.Token;
using MediatR;

namespace JustChat.Application.Features.Commands.ValidateToken
{
    public class ValidateTokenCommandHandler : IRequestHandler<ValidateTokenCommand, Token>
    {
        private readonly IDataUnitOfWork _data;

        public ValidateTokenCommandHandler(IDataUnitOfWork data)
        {
            _data = data;
        }

        public async Task<Token> Handle(ValidateTokenCommand request, CancellationToken cancellationToken)
        {
            var spec = TokenSpecification.GetNotExpiredByValue(request.Value);
            var token = await _data.Tokens.SingleOrDefaultAsync(spec);

            if (token == null)
            {
                throw new AuthenticationApplicationException();
            }

            return token;
        }
    }
}
