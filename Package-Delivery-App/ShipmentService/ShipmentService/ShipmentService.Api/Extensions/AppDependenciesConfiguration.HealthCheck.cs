using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.DependencyInjection;

namespace ShipmentService.Api.Extensions
{
    public static partial class AppDependenciesConfiguration
    {
        /// <summary>
        /// Configure the healthcheck service
        /// </summary>
        /// <param name="services">The services</param>
        /// <param name="configuration">The configuration</param>
        /// <returns>A <see cref="IServiceCollection"/></returns>
        //public static IServiceCollection AddHealthCheck(this IServiceCollection services,
        //   IConfiguration configuration)
        //{
        //    services
        //        .AddHealthCheck()
        //        .UseSqlServer(services.Configuration.GetConnectionString("ConnectionString"));

        //    return services;
        //}
    }
}
