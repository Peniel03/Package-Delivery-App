using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ShipmentService.DataAccess.Models;
namespace ShipmentService.DataAccess.Configurations
{
    /// <summary>
    /// configuration for the shipment model
    /// </summary>
    public class ShipmentConfiguration : IEntityTypeConfiguration<Shipment>
    {
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="builder"></param>
        public void Configure(EntityTypeBuilder<Shipment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(i => i.Id)
                .ValueGeneratedOnAdd();
            builder.Property(x => x.TrackingNumber)
                .IsRequired(true);
            builder.Property(x => x.TrackingNumber)
            .IsRequired(true);
            builder.Property(x => x.PickupDateTime)
            .IsRequired(true);
            builder.Property(x => x.DeliveryMethod)
            .IsRequired(true);
            builder.Property(x => x.EstimatedDeliveryDateTime)
            .IsRequired(true);
            builder.Property(x => x.ActualDeliveryDateTime)
            .IsRequired(true);
            builder.Property(x => x.TrackingNumber)
            .IsRequired(true);
            builder.Property(x => x.ShipmentCost)
            .IsRequired(true)
            .HasColumnType("decimal(18,2)");            ;
            builder.Property(x => x.ShipmentStatus)
            .IsRequired(true);
            //builder.OnDelete(DeleteBehavior.Cascade);
            builder.ToTable("Shipments");
        }
    }
}
