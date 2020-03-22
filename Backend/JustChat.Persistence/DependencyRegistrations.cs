using JustChat.Application.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace JustChat.Persistence
{
    public static class DependencyRegistrations
    {
        public static IServiceCollection RegisterPersistenceDepenencies(this IServiceCollection services, string commandDbConnectionString)
        {
            services.RegisterPersistanceCommandsDependencies(commandDbConnectionString);
            services.AddTransient<IDataUnitOfWork, DataUnitOfWork>();

            return services;
        }
    }
}
