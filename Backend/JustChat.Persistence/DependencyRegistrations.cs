using JustChat.Application.Interfaces;
using JustChat.Application.Interfaces.Repositories;
using JustChat.Persistence.Commands.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace JustChat.Persistence
{
    public static class DependencyRegistrations
    {
        public static IServiceCollection RegisterPersistenceDepenencies(this IServiceCollection services, string commandDbConnectionString)
        {
            services.RegisterPersistanceCommandsDependencies(commandDbConnectionString);

            services.AddTransient(typeof(IMutatableRepository<>), typeof(MutatableRepository<>));
            services.AddTransient<IDataUnitOfWork, DataUnitOfWork>();

            return services;
        }
    }
}
