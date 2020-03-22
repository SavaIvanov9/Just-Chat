using System.Threading.Tasks;
using JustChat.Api.Models.Messages;
using JustChat.Application.Commands.Messages.Create;
using MediatR;
using Microsoft.AspNetCore.SignalR;

namespace JustChat.Api.Hubs
{
    public class MessageHub : Hub
    {
        private readonly IMediator _mediator;

        public MessageHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task CreateMessage(CreateMessageRequest request)
        {
            var command = new CreateMessageCommand
            {
                UserId = request.UserId,
                RoomId = request.RoomId,
                Content = request.Content
            };

            var message = await _mediator.Send(command);

            var response = new CreateMessageResponse
            {
                UserId = message.UserId,
                Content = message.Content,
                Date = message.CreatedOn
            };

            await Clients.All.SendAsync("ReceiveMessage", response);
        }
    }
}
