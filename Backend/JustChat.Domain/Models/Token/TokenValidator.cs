using FluentValidation;
using JustChat.Domain.Models.Base;

namespace JustChat.Domain.Models.Token
{
    public class TokenValidator : EntityValidator<Token>
    {
        public TokenValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.Value)
                .NotEmpty();

            RuleFor(x => x.ValidFrom)
                .NotEmpty();

            RuleFor(x => x.ValidTo)
                .NotEmpty();
        }
    }
}
