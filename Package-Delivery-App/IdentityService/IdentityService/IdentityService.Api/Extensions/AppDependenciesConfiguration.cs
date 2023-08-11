using HealthChecks.UI.Client;
using IdentityService.Api.Mapping.Profiles;
using IdentityService.Api.Middlewares;
using IdentityService.Api.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using IdentityService.BusinessLogic.Interfaces;
using IdentityService.BusinessLogic.SeedData;
using IdentityService.BusinessLogic.Servcices;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using IdentityService.DataAccess.Extentions;

namespace IdentityService.Api.Extensions
{
    /// <summary>
    /// The configuration of services of the application
    /// </summary>
    public static class AppDependenciesConfiguration
    {

        /// <summary>
        /// Function to add services to the application builder
        /// </summary>
        /// <param name="builder">The application builder</param>
        /// <returns>A <see cref="WebApplicationBuilder"/></returns>
        public static WebApplicationBuilder ConfigureServicesApplication(this WebApplicationBuilder builder)
        {
            builder.AddLogger(builder.Configuration);
            builder.Services
                .AddIdentityServiceConfigutation(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("Sql"));
                })
                .AddAutoMapper(typeof(UserProfile), typeof(ClaimProfile))
                .AddScoped<IAccountService, AccountService>()
                .AddScoped<IAuthorizationService, AuthorizationService>()
                .AddScoped<ISessionService, SessionService>()
                .AddScoped<SeedUserRolesData>()
                //.AddHealthCheck(builder.Configuration)
                .AddValidatorsFromAssemblyContaining<UserCreateValidator>()
                .AddFluentValidationAutoValidation();

            return builder;
        }

        public static void Configure(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            // app.AddSeedData();

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


    }
}
