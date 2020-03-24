using System.Reflection;
using System.Threading.Tasks;
using FluentValidation.AspNetCore;
using JustChat.Api.Hubs;
using JustChat.Api.Middlewares;
using JustChat.Api.TokenValidation;
using JustChat.Application.Features.Commands.CreateMessage;
using JustChat.Mediator;
using JustChat.Persistence;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace JustChat.Api
{
    public class Startup
    {
        private const string _corsPolicyName = "default";
        private const string _messageHubEndpoint = "/api/chat";

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

            services.AddScoped<AuthGuardFilter>();

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            { 
                options.SecurityTokenValidators.Add(
                    new TokenValidator(services.BuildServiceProvider().GetRequiredService<IMediator>()));
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.Request.Query["access_token"];

                        var path = context.HttpContext.Request.Path;
                        if (string.IsNullOrEmpty(accessToken) == false
                            && path.StartsWithSegments(_messageHubEndpoint))
                        {
                            context.Token = accessToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            });

            services.AddSignalR();
            services.AddSingleton<IUserIdProvider, UserIdProvider>();

            services.AddDataProtection()
                .UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration()
                    {
                        EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
                        ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
                    });

            var commandDbConnectionString = Configuration.GetConnectionString("commandDbConnection");
            services.RegisterPersistenceDepenencies(commandDbConnectionString);
            services.RegisterApplicationDepenencies(Configuration);

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

            app.UseAuthentication();
            app.UseAuthorization();
  
            app.UseWhen(
                httpContext => httpContext.Request.Path.StartsWithSegments(_messageHubEndpoint) == false,
                x => x.UseMiddleware<ExceptionHandlingMiddleware>());

            //app.UseMiddleware<ExceptionHandlingMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<MessageHub>(_messageHubEndpoint);
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
