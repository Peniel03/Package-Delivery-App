using IdentityService.DataAccess.DataContext;
using IdentityService.DataAccess.Interfaces;
using IdentityService.DataAccess.Models;
using IdentityService.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.DataAccess.Extentions
{
    /// <summary>
    /// The Application services for the data access layer
    /// </summary>
    public static class AppDependenciesConfiguration
    {

        public static IServiceCollection AddIdentityServiceConfigutation(this IServiceCollection services, Action<DbContextOptionsBuilder> options)
        {
            services.AddDbContextPool<IdentityContext>(options)
                .AddIdentity<User, UserRole>(opts =>
                {
                    opts.User.RequireUniqueEmail = true;
                    opts.Password.RequireNonAlphanumeric = false;
                    opts.Password.RequireLowercase = true;
                    opts.Password.RequireUppercase = true;
                    opts.Password.RequireDigit = true;
                })
                .AddEntityFrameworkStores<IdentityContext>();

            services.AddScoped(serviceProvider =>
            serviceProvider.GetRequiredService<IdentityContext>().Set<UserRefreshToken>());
            services.AddScoped(serviceProvider =>
            serviceProvider.GetRequiredService<IdentityContext>().Set<UserClaim>());
            services.AddScoped<IUserClaimRepository, UserClaimRepository>();
            services.AddScoped<IUserRefreshTokenRepository, UserRefreshTokenRepository>();
            services.AddScoped<ISaveChangesRepository, SaveChangesRepository>();

            return services;
        }
    }
}
