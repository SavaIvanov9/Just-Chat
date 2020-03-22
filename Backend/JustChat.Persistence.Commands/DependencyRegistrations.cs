using JustChat.Application.Interfaces;
using JustChat.Persistence.Commands;
using JustChat.Persistence.Commands.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace JustChat.Persistence
{
    public static class DependencyRegistrations
    {
        public static IServiceCollection RegisterPersistanceCommandsDependencies(
            this IServiceCollection services, string tripsCommandDbConnectionString)
        {
            services.AddDbContext<CommandDbContext>(c => c.UseSqlServer(tripsCommandDbConnectionString));
            services.AddTransient(typeof(IMutatableRepository<>), typeof(MutatableRepository<>));

            return services;
        }
    }
}
