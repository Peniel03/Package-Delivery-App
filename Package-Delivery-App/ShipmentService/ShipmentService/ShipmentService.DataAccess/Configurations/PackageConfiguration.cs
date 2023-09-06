using ShipmentService.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
 

namespace ShipmentService.DataAccess.Configurations
{
    /// <summary>
    /// comnfiguration for the package model
    /// </summary>
    public class PackageConfiguration : IEntityTypeConfiguration<Package>
    {
        /// <summary>
        ///  <inheritdoc/>
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Package> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Id)
                   .ValueGeneratedOnAdd();

            builder.Property(x => x.Weight)
                .IsRequired(true)
            //[HasColumnType]-handles decimal to avoid précission loss
            .HasColumnType("decimal(18,2)");

            builder.Property(x => x.Dimensions)
               .IsRequired(true);
            builder.Property(x => x.ContentDescription)
               .IsRequired(true);

            builder.HasOne(x => x.Shipment)
                   .WithOne(x => x.Package)
                   .HasForeignKey<Shipment>(x => x.PackageId);

            builder.ToTable("Packages");




        }
    }
}
