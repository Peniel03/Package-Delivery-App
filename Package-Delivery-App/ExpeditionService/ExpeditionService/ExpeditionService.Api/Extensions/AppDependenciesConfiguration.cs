using ExpeditionService.Api.Mapping.Profiles;
using ExpeditionService.BusinessLogic.Interfaces;
using ExpeditionService.BusinessLogic.Logger;
using ExpeditionService.BusinessLogic.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Reflection;
using System.Text.Json;
using System.Text;
using ExpeditionService.Api.Middlewares;
using MongoFramework;
using ExpeditionService.Api.Validators;
using ExpeditionService.DataAccess.Extensions;
using FluentValidation;
using FluentValidation.AspNetCore;
using ExpeditionService.DataAccess.Models;

namespace ExpeditionService.Api.Extensions
{
    public static partial class AppDependenciesConfiguration
    {
        /// <summary>
        /// Function to configure the service of the web application
        /// Function to configure the service of the web application
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public static WebApplicationBuilder ConfigureServicesApplication(this WebApplicationBuilder builder, IConfiguration configuration)
        {
            var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            var dbName = Environment.GetEnvironmentVariable("DB_NAME");
            var dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");
            builder.Services
                .Configure<ExpeditionServiceMongoDbSettings>(
                    builder.Configuration.GetSection("ConnectionStrings"))
               .AddTransient<IMongoDbConnection, MongoDbConnection>(provider =>
               {
               var connectionString = builder.Configuration.GetConnectionString("MongoDBConnection");
                   // .Replace("{DB_HOST}", dbHost); 
                   return MongoDbConnection.FromConnectionString(connectionString);
               })
               .AddExpeditionDbContext(options =>
               {

               }
                )
                .AddRepositories()
                .AddAutoMapper(Assembly.GetAssembly(typeof(PackageProfile)))
                .AddScoped<IPersonService, PersonService>()
                .AddScoped<ILocationService, LocationService>()
                .AddScoped<IPackageService, PackageService>()
                .AddScoped<IShipmentService, ShipmentService>()
                .AddSingleton<ILoggerManager, LoggerManager>()
                .AddValidatorsFromAssemblyContaining<ShipmentCreateValidator>()
                .AddFluentValidationAutoValidation();

            return builder;
        }

        /// <summary>
        /// Function to configure the application
        /// </summary>
        /// <param name="app"><see cref="IApplicationBuilder"/></param>
        public static void Configure(this WebApplication app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }

        /// <summary>
        /// Function to write the response
        /// </summary>
        /// <param name="context">The http context</param>
        /// <param name="result">The result</param>
        /// <returns></returns>
        private static Task WriteResponse(HttpContext context, HealthReport result)
        {
            context.Response.ContentType = "application/json; charset=utf-8";
            var options = new JsonWriterOptions
            {
                Indented = true
            };
            using var stream = new MemoryStream();
            using (var writer = new Utf8JsonWriter(stream, options))
            {
                writer.WriteStartObject();
                writer.WriteString("status", result.Status.ToString());
                writer.WriteStartObject("results");
                foreach (var entry in result.Entries)
                {
                    writer.WriteStartObject(entry.Key);
                    writer.WriteString("status", entry.Value.Status.ToString());
                    writer.WriteEndObject();
                }
                writer.WriteEndObject();
                writer.WriteEndObject();
            }
            var json = Encoding.UTF8.GetString(stream.ToArray());
            return context.Response.WriteAsync(json);
        }
    }
}
