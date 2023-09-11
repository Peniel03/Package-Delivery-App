using ShipmentService.DataAccess.Models;

namespace ShipmentService.DataAccess.Interfaces
{
    /// <summary>
    /// The shipment repository interface to perfom additionals operations on shipment
    /// </summary>
    public interface IShipmentRepository:IBaseRepository<Shipment>
    {
        /// <summary>
        /// Function to get shipment by tracking number
        /// </summary>
        /// <param name="trackingNumber">the tracking number of the shipment</param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="Shipment"/></returns>
        Task<Shipment> GetShipmentByTrackingNumber(string trackingNumber, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get shipment by pick up date
        /// </summary>
        /// <param name="pickUpDateTime">the pickup date  of the shipment</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="Shipment"/></returns>
        Task<Shipment> GetShipmentByPickUpDateTime(DateTimeOffset pickUpDateTime ,CancellationToken cancellationToken);

        /// <summary>
        /// Function to get shipment by actual delivery date
        /// </summary>
        /// <param name="actualDeliveryDateTime">the actual delivery date of the shipment</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="Shipment"/></returns>
        Task<Shipment> GetShipmentByActualDeliveryDate(DateTimeOffset actualDeliveryDateTime, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get shipment by cost
        /// </summary>
        /// <param name="cost">the cost of the shipment</param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="Shipment"/></returns>
        Task<Shipment> GetShipmentByCost(decimal cost, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get shipment by status
        /// </summary>
        /// <param name="status">the status of the shipment</param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="Shipment"/> </returns>
        Task<Shipment> GetShipmentByStatus(string status, CancellationToken cancellationToken);


    }
}
