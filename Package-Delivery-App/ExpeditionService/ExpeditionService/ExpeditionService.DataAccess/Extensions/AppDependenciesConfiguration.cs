using ExpeditionService.DataAccess.DataContext;
using ExpeditionService.DataAccess.Interfaces;
using ExpeditionService.DataAccess.Models;
using ExpeditionService.DataAccess.Repositories;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionService.DataAccess.Extensions
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

        //public static IServiceCollection AddShipmentDbContext(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        //{

        //    services.AddDbContextPool<ExpeditionContext>(options);

        //    services.AddScoped(serviceProvider => serviceProvider.GetRequiredService<ExpeditionContext>().Set<Person>());
        //    services.AddScoped(serviceProvider => serviceProvider.GetRequiredService<ExpeditionContext>().Set<Location>());
        //    services.AddScoped(serviceProvider => serviceProvider.GetRequiredService<ExpeditionContext>().Set<Package>());
        //    services.AddScoped(serviceProvider => serviceProvider.GetRequiredService<ExpeditionContext>().Set<Shipment>());

        //    return services;
        //}

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
                           .AddScoped<ISaveChangesRepository, SaveChangesRepository>();

        }

    }
}
