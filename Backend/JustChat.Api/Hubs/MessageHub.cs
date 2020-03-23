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
using Microsoft.AspNetCore.SignalR;

namespace JustChat.Api.Hubs
{
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
                        UserId = request.UserId,
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
                    .Group(message.RoomId)
                    .SendAsync(_receiveMessageName, response);
            }

            await HandleError(ProcessRequest);
        }

        public async Task AddToGroup(string roomId)
        {
            async Task ProcessRequest()
            {
                var room = await _mediator.Send(new GetRoomQuery { Id = roomId });

                await Groups.AddToGroupAsync(Context.ConnectionId, room.Id);

                await Clients
                    .Group(room.Id)
                    .SendAsync(_receiveMessageName, $"{Context.ConnectionId} has joined the room {room.Name}.");
            }

            await HandleError(ProcessRequest);
        }

        public async Task RemoveFromGroup(string roomId)
        {
            async Task ProcessRequest()
            {
                var room = await _mediator.Send(new GetRoomQuery { Id = roomId });

                await Groups.RemoveFromGroupAsync(Context.ConnectionId, room.Id);

                await Clients
                    .Group(room.Id)
                    .SendAsync(_receiveMessageName, $"{Context.ConnectionId} has left the room {room.Name}.");
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
            catch(Exception exception)
            {
                await Clients.Caller.SendAsync(_handleErrorName, exception.ToString());
            }
        }
    }
}
