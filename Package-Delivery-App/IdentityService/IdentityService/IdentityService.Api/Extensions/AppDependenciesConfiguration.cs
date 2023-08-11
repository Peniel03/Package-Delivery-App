using IdentityService.Api.Mapping.Profiles;
using IdentityService.Api.Middlewares;
using IdentityService.Api.Validators;
using Microsoft.EntityFrameworkCore;
using IdentityService.BusinessLogic.Interfaces;
using IdentityService.BusinessLogic.SeedData;
using IdentityService.BusinessLogic.Servcices;
using IdentityService.DataAccess.Extentions;
using FluentValidation;
using FluentValidation.AspNetCore;
using IdentityService.DataAccess.DataContext;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

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
        public static WebApplicationBuilder ConfigureServicesApplication(this WebApplicationBuilder builder,IConfiguration configuration)
        {

             builder.Services
                .AddIdentityServiceConfigutation(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                })
                
                .AddAutoMapper(typeof(UserProfile), typeof(ClaimProfile))
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IAuthorizationService, AuthorizationService>()
                .AddScoped<ISessionService, SessionService>()
                .AddScoped<SeedUserRolesData>()
                .AddValidatorsFromAssemblyContaining<UserCreateValidator>()
                .AddFluentValidationAutoValidation();

            return builder ;
        }

        public static void Configure(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            app.MapHealthChecks("/health", new HealthCheckOptions
            {
                ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            });
            app.AddSeedData();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

 
            app.UseHttpsRedirection();

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
