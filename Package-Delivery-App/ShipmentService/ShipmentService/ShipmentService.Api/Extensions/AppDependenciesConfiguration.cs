using System.Text.Json;
using System.Text;
using Microsoft.AspNetCore.Diagnostics;
using ShipmentService.BusinessLogic.Interfaces;
using ShipmentService.BusinessLogic.Services;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using ShipmentService.Api.Mapping.Profiles;
using ShipmentService.Api.Validators;
using ShipmentService.DataAccess.Extensions;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using FluentValidation;
using FluentValidation.AspNetCore;
using ShipmentService.BusinessLogic.Logger;

namespace ShipmentService.Api.Extensions
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
        public static  WebApplicationBuilder ConfigureServicesApplication(this WebApplicationBuilder builder, IConfiguration configuration)
        {


            builder.Services
                .AddShipmentDbContext(options =>
                {
                    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
                })


                .AddRepositories()
                .AddAutoMapper(Assembly.GetAssembly(typeof(PersonProfile)))
                .AddScoped<IPersonService, PersonService>()
                .AddScoped<ILocationService, LocationService>()
                .AddScoped<IPackageService, PackageService>()
                .AddScoped<ILoggerManager, LoggerManager>()
                //.AddHealthCheck(builder.Configuration)           
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

            //app.MapHealthChecks("/health", new HealthCheckOptions()
            //{
            //    Predicate = (check) => !check.Tags.Contains("services"),
            //    AllowCachingResponses = false,
            //    ResponseWriter = WriteResponse,
            //});
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
