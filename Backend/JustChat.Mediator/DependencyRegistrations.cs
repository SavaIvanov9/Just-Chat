using System;
using System.Reflection;
using JustChat.Mediator.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace JustChat.Mediator
{
    public static class DependencyRegistrations
    {
        public static IServiceCollection RegisterMediator(
            this IServiceCollection services,
            Assembly[] assemblies,
            Action<IMediatorBuilder> action = null)
        {
            var mediatorBuilder = new MediatorBuilder(services, assemblies);

            action?.Invoke(mediatorBuilder);

            return services;
        }
    }
}