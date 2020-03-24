using FluentValidation;
using JustChat.Application.Interfaces;
using JustChat.Application.Validation;

namespace JustChat.Application.Features.Commands.LogInUser
{
    public class LogInUserCommandValidator : RequestValidator<LogInUserCommand>
    {
        public LogInUserCommandValidator(IDataUnitOfWork data)
        {
            RuleFor(x => x.UserName)
                .NotEmpty();

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}