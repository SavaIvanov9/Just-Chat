using FluentValidation;
using JustChat.Application.Validation;

namespace JustChat.Application.Features.Commands.ValidateToken
{
    public class ValidateTokenCommandValidator : RequestValidator<ValidateTokenCommand>
    {
        public ValidateTokenCommandValidator()
        {
            RuleFor(x => x.Value)
                .NotEmpty();
        }
    }
}