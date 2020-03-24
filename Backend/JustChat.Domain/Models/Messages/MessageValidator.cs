using FluentValidation;
using JustChat.Domain.Models.Base;

namespace JustChat.Domain.Models.Messages
{
    public class MessageValidator : EntityValidator<Message>
    {
        public MessageValidator()
        {
            RuleFor(x => x.UserId)
                .NotEmpty();

            RuleFor(x => x.RoomId)
                .NotEmpty();

            RuleFor(x => x.Content)
                .NotEmpty();
        }
    }
}
