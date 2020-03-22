using System;
using System.Threading;
using System.Threading.Tasks;
using JustChat.Application.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace JustChat.Mediator.Behaviors
{
    public class LoggingBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

        public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
        {
            _logger = logger;
        }

        public async Task<TResponse> Handle(
            TRequest request,
            CancellationToken cancellationToken,
            RequestHandlerDelegate<TResponse> next)
        {
            if (!request.GetType().IsSubclassOf(typeof(ICommand<TResponse>)))
            {
                return await next();
            }

            var commandName = request.GetType().Name;
            try
            {
                _logger.LogInformation("Handling command {CommandName} {@Command}", commandName, request);
                var response = await next();
                _logger.LogInformation("Command {CommandName} handled - response: {@Response}", commandName, response);

                return response;
            }
            catch (Exception e)
            {
                _logger.LogInformation(e, "Command {CommandName} failed");
                throw;
            }
        }
    }
}