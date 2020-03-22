using System;
using System.Linq;
using System.Reflection;
using JustChat.Application.Interfaces;
using JustChat.Mediator.Behaviors;
using JustChat.Mediator.Interfaces;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace JustChat.Mediator
{
    internal class MediatorBuilder : IMediatorBuilder
    {
        private readonly IServiceCollection _services;

        public MediatorBuilder(IServiceCollection services, params Assembly[] assemblies)
        {
            _services = services;
            RegisterMediator(assemblies);
        }

        public IMediatorBuilder WithValidationBehavior()
        {
            _services.AddTransient(
                typeof(IPipelineBehavior<,>),
                typeof(ValidationBehavior<,>));

            return this;
        }

        public IMediatorBuilder WithLoggingBehavior()
        {
            _services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingBehavior<,>));
            return this;
        }

        public IMediatorBuilder WithPersistableBehavior()
        {
            if (_services.All(n => n.ServiceType != typeof(IDataUnitOfWork)))
            {
                throw new InvalidOperationException(
                    $"You must register {nameof(IDataUnitOfWork)} before registering {typeof(PersistableBehavior<,>).Name}.");
            }

            _services.AddTransient(typeof(IPipelineBehavior<,>), typeof(PersistableBehavior<,>));

            return this;
        }

        private void RegisterMediator(params Assembly[] assemblies)
        {
            _services.AddMediatR(assemblies);
        }
    }
}
