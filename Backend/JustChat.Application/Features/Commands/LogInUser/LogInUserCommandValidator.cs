using FluentValidation;
using JustChat.Application.Interfaces;
using JustChat.Application.Validation;
using JustChat.Domain.Models.Users;

namespace JustChat.Application.Features.Commands.LogInUser
{
    public class LogInUserCommandValidator : RequestValidator<LogInUserCommand>
    {
        public LogInUserCommandValidator(IDataUnitOfWork data)
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .SetValidator(new UsernameValidator());

            RuleFor(x => x.Password)
                .NotEmpty()
                .SetValidator(new PasswordValidator());
        }
    }
}