using FluentValidation;
using JustChat.Application.Extensions;
using JustChat.Application.Interfaces;
using JustChat.Application.Specifications;
using JustChat.Application.Validation;

namespace JustChat.Application.Commands.Users.Create
{
    public class CreateUserCommandValidator : RequestValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator(IDataUnitOfWork data)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .DoesNotExist(UserSpecification.FindByName, data.Users);
        }
    }
}
