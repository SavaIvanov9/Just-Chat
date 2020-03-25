using FluentValidation;
using JustChat.Domain.Models.Base;

namespace JustChat.Domain.Models.Users
{
    public class UserValidator : EntityValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .SetValidator(new UsernameValidator());

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}
