using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using JustChat.Api.Models;
using JustChat.Api.Models.Messages;
using JustChat.Application.Commands.Messages.Create;
using JustChat.Application.Exceptions;
using JustChat.Domain.Exceptions;
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
            async Task ProcessRequest()
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

                await Clients.Caller.SendAsync("HandleError", response);
            }
            catch (ValidationDomainException exception)
            {
                var response = new ValidationErrorResponse(
                    exception.Message,
                    new Dictionary<string, string[]>
                    {
                        { exception.PropertyName, new[] { exception.Message } }
                    });

                await Clients.Caller.SendAsync("HandleError", response);
            }
            catch(Exception exception)
            {
                await Clients.Caller.SendAsync("HandleError", exception.ToString());
            }
        }
    }
}
