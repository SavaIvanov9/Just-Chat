using System.Threading;
using System.Threading.Tasks;
using JustChat.Application.Interfaces;
using JustChat.Domain.Models.Rooms;
using MediatR;

namespace JustChat.Application.Features.Commands.Messages.Create
{
    public class CreateMessageCommandHandler : IRequestHandler<CreateMessageCommand, Message>
    {
        private readonly IDataUnitOfWork _data;

        public CreateMessageCommandHandler(IDataUnitOfWork data)
        {
            _data = data;
        }

        public async Task<Message> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
        {
            var message = new Message(request.UserId, request.RoomId, request.Content);
            return await _data.Messages.AddAsync(message);
        }
    }
}
