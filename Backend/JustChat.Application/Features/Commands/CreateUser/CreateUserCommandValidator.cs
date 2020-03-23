using FluentValidation;
using JustChat.Application.Extensions;
using JustChat.Application.Interfaces;
using JustChat.Application.Models.Specifications;
using JustChat.Application.Validation;

namespace JustChat.Application.Features.Commands.CreateUser
{
    public class CreateUserCommandValidator : RequestValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator(IDataUnitOfWork data)
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .DoesNotExist(UserSpecification.GetByName, data.Users);
        }
    }
}
