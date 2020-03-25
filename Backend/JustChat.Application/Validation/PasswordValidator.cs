using FluentValidation;

namespace JustChat.Application.Validation
{
    public class PasswordValidator : AbstractValidator<string>
    {
        public const string PasswordRegex = "^[a-zA-Z0-9!@#$%^&*()_+]*$";

        public PasswordValidator()
        {
            CascadeMode = CascadeMode.StopOnFirstFailure;

            RuleFor(x => x)
                .NotEmpty()
                .MinimumLength(5)
                .MaximumLength(50)
                .Matches(PasswordRegex);
        }
    }
}
