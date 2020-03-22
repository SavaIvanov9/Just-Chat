using System.Threading;
using System.Threading.Tasks;
using JustChat.Domain.Models.Rooms;
using MediatR;

namespace JustChat.Application.Commands.Messages.Create
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Message>
    {
        public Task<Message> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            return null;
        }
    }
}
