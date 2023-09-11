using ShipmentService.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
 

namespace ShipmentService.DataAccess.Configurations
{
    /// <summary>
    /// configuration for the location model 
    /// </summary>
    public class LocationConfiguration : IEntityTypeConfiguration<Location>
    {
        /// <summary>
        ///  <inheritdoc/>
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Location> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(i => i.Id)
                .ValueGeneratedOnAdd();

            builder.Property(x => x.LocationName)
                .IsRequired(true);
            builder.Property(x => x.Address)
               .IsRequired(true);
            builder.Property(x => x.City)
               .IsRequired(true);
            builder.Property(x => x.Country)
               .IsRequired(true);
            builder.Property(x => x.PostalCode)
               .IsRequired(true);

            builder.HasMany(x => x.Persons)
                   .WithOne(x => x.Location)
                   .HasForeignKey(x => x.LocationId);


            builder.HasMany(x => x.Shipements)
                   .WithOne(x => x.Location)
                   .HasForeignKey(x => x.PickUpLocationId);

            builder.HasMany(x => x.Shipements)
                   .WithOne(x => x.Location)
                   .HasForeignKey(x => x.DestinationLocationId);

            builder.ToTable("Locations");

        }
    }
}
