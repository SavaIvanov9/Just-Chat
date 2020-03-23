using JustChat.Application.Interfaces;
using JustChat.Application.Interfaces.Repositories;
using JustChat.Persistence.Commands.Repositories;
using JustChat.Persistence.Interfaces;
using JustChat.Persistence.Repositories;
using JustChat.Persistence.Services;
using Microsoft.Extensions.DependencyInjection;

namespace JustChat.Persistence
{
    public static class DependencyRegistrations
    {
        public static IServiceCollection RegisterPersistenceDepenencies(this IServiceCollection services, string commandDbConnectionString)
        {
            services.RegisterPersistanceCommandsDependencies(commandDbConnectionString);
            
            services.AddTransient(typeof(ISpecificationEvaluationService<>), typeof(SpecificationEvaluationService<>));
            services.AddTransient<IDataSeedingService, DataSeedingService>();
            services.AddTransient(typeof(IReadableRepository<>), typeof(ReadableRepository<>));
            services.AddTransient(typeof(IMutatableRepository<>), typeof(MutatableRepository<>));
            services.AddTransient<IDataUnitOfWork, DataUnitOfWork>();

            return services;
        }
    }
}
