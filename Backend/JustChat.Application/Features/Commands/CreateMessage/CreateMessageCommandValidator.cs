using FluentValidation;
using JustChat.Application.Extensions;
using JustChat.Application.Interfaces;
using JustChat.Application.Models.Specifications;
using JustChat.Application.Validation;

namespace JustChat.Application.Features.Commands.CreateMessage
{
    public class CreateMessageCommandValidator : RequestValidator<CreateMessageCommand>
    {
        public CreateMessageCommandValidator(IDataUnitOfWork data)
        {
            RuleFor(x => x.UserId)
                .NotEmpty()
                .Exists(UserSpecification.GetById, data.Users);

            RuleFor(x => x.RoomId)
                .NotEmpty()
                .Exists(RoomSpecification.GetById, data.Rooms);

            RuleFor(x => x.Content)
                .NotEmpty();
        }
    }
}
