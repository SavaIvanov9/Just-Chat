using FluentValidation;
using JustChat.Domain.Models.Base;

namespace JustChat.Domain.Models.Users
{
    public class UserValidator : EntityValidator<User>
    {
        public const string PasswordRegex = "^[a-zA-Z0-9!@#$%^&*()_+]*$";
        public const string NameRegex = "^[a-zA-Z0-9-.]*$";
        public UserValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(50)
                .Matches(NameRegex);

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(50)
                .Matches(PasswordRegex);
        }
    }
}
