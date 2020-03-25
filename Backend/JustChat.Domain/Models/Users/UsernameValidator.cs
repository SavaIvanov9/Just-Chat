using FluentValidation;

namespace JustChat.Domain.Models.Users
{
    public class UsernameValidator : AbstractValidator<string>
    {
        public const string NameRegex = "^[a-zA-Z0-9-.]*$";

        public UsernameValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x)
                .NotEmpty()
                .MinimumLength(1)
                .MaximumLength(50)
                .Matches(NameRegex);
        }
    }
}
