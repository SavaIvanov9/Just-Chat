using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using JustChat.Api.Hubs;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using JustChat.Persistence;
using JustChat.Mediator;
using System.Reflection;
using FluentValidation.AspNetCore;
using JustChat.Application.Commands.Messages.Create;
using Microsoft.OpenApi.Models;

namespace JustChat.Api
{
    public class Startup
    {
        private const string _corsPolicyName = "default";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers()
                .AddFluentValidation(c => c.RegisterValidatorsFromAssemblyContaining<CreateMessageCommandValidator>());

            services.AddCors(options =>
            {
                options.AddPolicy(
                    _corsPolicyName,
                    builder => builder
                        .AllowAnyMethod()
                        .AllowAnyHeader()
                        .SetIsOriginAllowed((host) => true)
                        .AllowCredentials());
            });

            services.AddSignalR();

            var commandDbConnectionString = Configuration.GetConnectionString("commandDbConnection");
            services.RegisterPersistenceDepenencies(commandDbConnectionString);

            RegisterMediator(services);
            RegisterSwagger(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Swagger");
                c.RoutePrefix = string.Empty;
            });

            app.UseRouting();
            app.UseCors(_corsPolicyName);
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>("/api/MessageHub");
            });
        }

        private void RegisterSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Just Chat API", Version = "v1" });
            });
        }


        private void RegisterMediator(IServiceCollection services)
        {
            services.RegisterMediator(
               new[]
               {
                    typeof(CreateMessageCommandHandler).GetTypeInfo().Assembly,
               },
               configure =>
               {
                   configure
                       .WithLoggingBehavior()
                       .WithPersistableBehavior()
                       .WithValidationBehavior();
               });
        }
    }
}
