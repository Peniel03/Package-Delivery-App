using ShipmentService.BusinessLogic.DTOs;
using ShipmentService.DataAccess.Models;

namespace ShipmentService.BusinessLogic.Interfaces
{
    /// <summary>
    /// The shipment service interface to perfom additionals operations on shipment
    /// </summary>
    public interface IShipmentService: IBaseService<ShipmentDto>
    {
        /// <summary>
        /// Function to get shipment by tracking number
        /// </summary>
        /// <param name="shipmentDto">the entity that will help us to access the tracking number in the record 
        /// where we want to get the shipment from</param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="Shipment"/></returns>
        Task<ShipmentDto> GetShipmentByTrackingNumberAsync(ShipmentDto shipmentDto, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get shipment by pick up date
        /// </summary>
        /// <param name="shipmentDto">the entity that will help us to access the pickup date in the record 
        /// where we want to get the shipment from</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="Shipment"/></returns>
        Task<ShipmentDto> GetShipmentByPickUpDateTimeAsync(ShipmentDto shipmentDto, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get shipment by actual delivery date
        /// </summary>
        /// <param name="shipmentDto">the entity that will help us to access the actual delivery date in the record 
        /// where we want to get the shipment from</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="Shipment"/></returns>
        Task<ShipmentDto> GetShipmentByActualDeliveryDateAsync(ShipmentDto shipmentDto, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get shipment by cost
        /// </summary>
        /// <param name="shipmentDto">the entity that will help us to access the cost in the record 
        /// where we want to get the shipment from</param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="Shipment"/></returns>
        Task<ShipmentDto> GetShipmentByCostAsync(ShipmentDto shipmentDto, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get shipment by status
        /// </summary>
        /// <param name="shipmentDto">the entity that will help us to access the status in the record 
        /// where we want to get the shipment from</param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="Shipment"/> </returns>
        Task<ShipmentDto> GetShipmentByStatusAsync(ShipmentDto shipmentDto, CancellationToken cancellationToken);
         
    }
}
