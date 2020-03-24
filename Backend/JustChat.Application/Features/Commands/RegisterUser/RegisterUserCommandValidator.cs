using FluentValidation;
using JustChat.Application.Extensions;
using JustChat.Application.Interfaces;
using JustChat.Application.Models.Specifications;
using JustChat.Application.Validation;

namespace JustChat.Application.Features.Commands.RegisterUser
{
    public class RegisterUserCommandValidator : RequestValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator(IDataUnitOfWork data)
        {
            RuleFor(x => x.UserName)
                .NotEmpty()
                .DoesNotExist(UserSpecification.GetByName, data.Users);

            RuleFor(x => x.Password)
                .NotEmpty();
        }
    }
}