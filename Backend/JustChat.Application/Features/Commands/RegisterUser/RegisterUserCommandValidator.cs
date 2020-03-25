using FluentValidation;
using JustChat.Application.Extensions;
using JustChat.Application.Interfaces;
using JustChat.Application.Models.Specifications;
using JustChat.Application.Validation;
using JustChat.Domain.Models.Users;

namespace JustChat.Application.Features.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : RequestValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator(IDataUnitOfWork data)
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .SetValidator(new UsernameValidator())
                .DoesNotExist(UserSpecification.GetByName, data.Users);

            RuleFor(x => x.Password)
                .NotEmpty()
                .SetValidator(new PasswordValidator());
        }
    }
}