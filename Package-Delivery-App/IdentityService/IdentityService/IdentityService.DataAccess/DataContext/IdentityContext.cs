using IdentityService.DataAccess.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.DataAccess.DataContext
{
    public class IdentityContext: IdentityDbContext<User,UserRole,int,UserClaim,
        IdentityUserRole<int>,
        IdentityUserLogin<int>, 
        IdentityRoleClaim<int>,
        IdentityUserToken<int>>
    {
        /// <summary>
        /// Initializes a new instance of <see cref="IdentityContext"/>
        /// </summary>
        /// <param name="options"></param>
        public IdentityContext(DbContextOptions<IdentityContext> options) : base (options)
        {

            
        }
        /// <summary>
        /// Overrding the  OnModelCreating of the IdentityDbContext to add our configuration
        /// </summary>
        /// <param name="modelBuilder">The modelBuilder of the database context</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(IdentityContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

    }
}
