using JustChat.Application.Interfaces;
using MediatR;

namespace JustChat.Application.Commands.Messages.CreateMessage
{
    public class CreateMessageCommand : ICommand<Unit>
    {
        public string UserId { get; set; }

        public string RoomId { get; set; }
        
        public string Content { get; set; }
    }
}
