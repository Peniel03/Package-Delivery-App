using IdentityService.Api.Mapping.Profiles;
using IdentityService.Api.Middlewares;
using IdentityService.Api.Validators;
using Microsoft.EntityFrameworkCore;
using IdentityService.BusinessLogic.Interfaces;
using IdentityService.BusinessLogic.Servcices;
using IdentityService.DataAccess.Extentions;
using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityService.DataAccess.DataContext;
using IdentityService.DataAccess.Data.SeedData;
using IdentityService.BusinessLogic.Logger;
using System.Reflection;
using MassTransit;

namespace IdentityService.Api.Extensions
{
    /// <summary>
    /// The configuration of services of the application
    /// </summary>
    public partial class AppDependenciesConfiguration
    {
        /// <summary>
        /// Function to add services to the application builder
        /// </summary>
        /// <param name="builder">The application builder</param>
        /// <returns>A <see cref="WebApplicationBuilder"/></returns>
        public static IServiceCollection ConfigureApplicationServices(this WebApplicationBuilder builder,IConfiguration configuration)
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

            builder.Services
                .AddIdentityServiceConfigutation(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                        //.Replace("{DB_HOST}", dbHost)
                        //.Replace("{DB_NAME}", dbName)
                        //.Replace("{DB_SA_PASSWORD}", dbPassword));
                })
                .AddAutoMapper(Assembly.GetAssembly(typeof(UserProfile))) 
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IAuthorizationService, AuthorizationService>()
                .AddScoped<ISessionService, SessionService>()
                .AddTransient<SeedUserRolesData>()
                .AddSingleton<ILoggerManager, LoggerManager>()
                .AddValidatorsFromAssemblyContaining<UserCreateValidator>()
                .AddFluentValidationAutoValidation()
                .AddExceptionHandler(options => {
                options.ExceptionHandlingPath = "/Error";
            });      
            return builder.Services ;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static IServiceCollection ConfigureMassTransit(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                // x.AddConsumer<UserRegisteredConsumer>();
                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host("rabbitmq://localhost:15672/", h =>
                    {
                        h.Username("guest");
                        h.Password("guest");
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });
            return services;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="app"></param>
        public static void Configure(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.AddSeedData();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
         }

        /// <summary>
        /// Function to add seed data to the application
        /// </summary>
        /// <param name="app">The application</param>
        public static void AddSeedData(this WebApplication app)
        {
            var serviceScopeFactory = app.Services.GetRequiredService<IServiceScopeFactory>();
            using (var scope = serviceScopeFactory.CreateScope())
            {
                var handler = scope.ServiceProvider.GetRequiredService<IdentityContext>();
                handler.Database.Migrate();    
                var service = scope.ServiceProvider.GetRequiredService<SeedUserRolesData>();
                service.SeedData();
            } 
        }
    }
}
