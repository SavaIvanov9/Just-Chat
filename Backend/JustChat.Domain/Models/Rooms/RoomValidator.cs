using FluentValidation;
using JustChat.Domain.Models.Base;

namespace JustChat.Domain.Models.Rooms
{
    public class RoomValidator : EntityValidator<Room>
    {
        public RoomValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty();

            RuleFor(x => x.Type)
                .NotEmpty();
        }
    }
}
