using IdentityService.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.DataAccess.Configurations
{
    /// <summary>
    /// Configuration of the user Refresh token table
    /// </summary>
    public class UserRefressTokenConfiguration : IEntityTypeConfiguration<UserRefreshToken>
    {
        /// <summary>
        /// Configure the refresh token table
        /// </summary>
        /// <param name="builder">the builder</param>
        public void Configure(EntityTypeBuilder<UserRefreshToken> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.RefreshToken)
                .IsRequired(true);
            builder.Property(x => x.LifeRefreshTokenInMinutes)
                .IsRequired(true);
            builder.Property(x => x.CreationDate)
                .IsRequired(true);
            builder.Ignore(x => x.IsActive);
            builder.HasOne(x => x.User)
                .WithMany(x => x.UserRefreshTokens);

            builder.ToTable("UserRefreshTokens");

        }
    }
}
