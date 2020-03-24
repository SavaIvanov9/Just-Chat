using JustChat.Application.Interfaces;
using JustChat.Domain.Models.Messages;

namespace JustChat.Application.Features.Commands.CreateMessage
{
    public class CreateMessageCommand : ICommand<Message>
    {
        public long UserId { get; set; }

        public long RoomId { get; set; }

        public string Content { get; set; }
    }
}
