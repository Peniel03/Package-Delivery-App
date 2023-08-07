using IdentityService.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.DataAccess.Configurations
{
    /// <summary>
    /// Configuration of the role of the user
    /// </summary>
    public class IdentityRoleConfiguration : IEntityTypeConfiguration<UserRole>
    {
        /// <summary>
        /// function  to configure the role of the user
        /// </summary>
        /// <param name="builder"> The builder <see cref="EntityTypeBuilder"/></param>
        public void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Name).IsRequired();

            builder.ToTable("UserRoles");
         }
    }
}
