using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ShipmentService.DataAccess.DataContext;
using ShipmentService.DataAccess.Interfaces;
using ShipmentService.DataAccess.Models;
using ShipmentService.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.DataAccess.Extensions
{
    /// <summary>
    /// The application services for the data access layer
    /// </summary>
    public static partial class AppDependenciesConfiguration
    {
        /// <summary>
        /// Function to add the context services (services of the data access layer)
        /// </summary>
        /// <param name="services"></param>
        /// <param name="options"></param>
        /// <returns></returns>
        public static IServiceCollection AddShipmentDbContext(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {

            services.AddDbContextPool<ShipmentContext>(options);

            services.AddScoped(serviceProvider => serviceProvider.GetRequiredService<ShipmentContext>().Set<Person>());
            services.AddScoped(serviceProvider => serviceProvider.GetRequiredService<ShipmentContext>().Set<Location>());
            services.AddScoped(serviceProvider => serviceProvider.GetRequiredService<ShipmentContext>().Set<Package>());
            services.AddScoped(serviceProvider => serviceProvider.GetRequiredService<ShipmentContext>().Set<Shipment>());

            return services;
        }

        /// <summary>
        /// function to add repositories to the services
        /// </summary>
        /// <param name="services"></param>
        /// <returns></returns>
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services.AddScoped<IPersonRepository, PersonRepository>()
                           .AddScoped<ILocationRepository, LocationRepository>()
                           .AddScoped<IPackageRepository, PackageRepository>()
                           .AddScoped<IShipmentRepository, ShipmentRepository>()
                           .AddScoped<ISaveChangesRepository,SaveChangesRepository>();

        }

    }
}
