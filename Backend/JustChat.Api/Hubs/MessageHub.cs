using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JustChat.Api.Models;
using JustChat.Api.Models.Messages;
using JustChat.Application.Exceptions;
using JustChat.Application.Features.Commands.CreateMessage;
using JustChat.Application.Features.Queries.GetRoom;
using JustChat.Domain.Exceptions;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace JustChat.Api.Hubs
{
    [Authorize]
    public class MessageHub : Hub
    {
        private readonly IMediator _mediator;
        private readonly string _receiveMessageName = "ReceiveMessage";
        private readonly string _handleErrorName = "HandleError";

        public MessageHub(IMediator mediator)
        {
            _mediator = mediator;
        }

        public async Task CreateMessage(CreateMessageRequest request)
        {
            async Task ProcessRequest()
            {
                var message = await _mediator
                    .Send(new CreateMessageCommand
                    {
                        UserId = long.Parse(Context.UserIdentifier),
                        RoomId = request.RoomId,
                        Content = request.Content
                    });

                var response = new CreateMessageResponse
                {
                    UserId = message.UserId,
                    Content = message.Content,
                    Date = message.CreatedOn
                };

                await Clients
                    .Group(message.RoomId.ToString())
                    .SendAsync(_receiveMessageName, response);
            }

            await HandleError(ProcessRequest);
        }

        public async Task JoinRoom(long roomId)
        {
            async Task ProcessRequest()
            {
                var room = await _mediator.Send(new GetRoomQuery { Id = roomId });

                await Groups.AddToGroupAsync(Context.ConnectionId, room.Id.ToString());

                var response = new CreateMessageResponse
                {
                    Content = $"{Context.User?.Identity?.Name} has joined the room {room.Name}.",
                    Date = DateTime.UtcNow
                };

                await Clients
                    .Group(room.Id.ToString())
                    .SendAsync(_receiveMessageName, response);
            }

            await HandleError(ProcessRequest);
        }

        public async Task LeaveRoom(long roomId)
        {
            async Task ProcessRequest()
            {
                var room = await _mediator.Send(new GetRoomQuery { Id = roomId });

                await Groups.RemoveFromGroupAsync(Context.ConnectionId, room.Id.ToString());

                var response = new CreateMessageResponse
                {
                    Content = $"{Context.User?.Identity?.Name} has left the room {room.Name}.",
                    Date = DateTime.UtcNow
                };

                await Clients
                    .Group(room.Id.ToString())
                    .SendAsync(_receiveMessageName, response);
            }

            await HandleError(ProcessRequest);
        }

        private async Task HandleError(Func<Task> processRequest)
        {
            try
            {
                await processRequest();
            }
            catch (ValidationApplicationException exception)
            {
                var response = new ValidationErrorResponse(
                    exception.Message, exception.Failures);

                await Clients.Caller.SendAsync(_handleErrorName, response);
            }
            catch (ValidationDomainException exception)
            {
                var response = new ValidationErrorResponse(
                    exception.Message,
                    new Dictionary<string, string[]>
                    {
                        { exception.PropertyName, new[] { exception.Message } }
                    });

                await Clients.Caller.SendAsync(_handleErrorName, response);
            }
            catch (Exception exception)
            {
                await Clients.Caller.SendAsync(_handleErrorName, exception.ToString());
            }
        }
    }
}
