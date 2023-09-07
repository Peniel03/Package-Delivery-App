using IdentityService.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace IdentityService.DataAccess.Configurations
{
    /// <summary>
    /// Configuration for the user
    /// </summary>
    public class IdentityUserConfiguration : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Function to confugure the user
        /// </summary>
        /// <param name="builder">The builder <see cref="EntityTypeBuilder"/</param>
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.PhoneNumber).IsRequired();
            builder.Property(x => x.PasswordHash).IsRequired();
            builder.ToTable("Users"); 
        }
    }
}
