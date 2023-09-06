using ShipmentService.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
 

namespace ShipmentService.DataAccess.Configurations
{
    /// <summary>
    /// Configuration for the person model
    /// </summary>
    public class PersonConfiguration : IEntityTypeConfiguration<Person>
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="builder">the builder</param>
        public void Configure(EntityTypeBuilder<Person> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(i => i.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.Name)
                .IsRequired(true);
            builder.Property(x => x.Phone)
                .IsRequired(true);

            builder.HasMany(x => x.Packages)
                   .WithOne(x => x.Person)
                   .HasForeignKey(x => x.OwnerId);

            builder.HasMany(x => x.shipments)
                   .WithOne(x => x.Person)
                   .HasForeignKey(x => x.SenderId);

            builder.HasMany(x => x.shipments)
                   .WithOne(x => x.Person)
                   .HasForeignKey(x => x.RecipientId);

            builder.ToTable("Persons");

        }
    }
}
