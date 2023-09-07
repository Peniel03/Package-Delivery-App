using ShipmentService.BusinessLogic.DTOs;

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
        /// <param name="trackingnumber">the tracking number of the shipment that we want to get
        /// <returns>A <see cref="Task"/>that contains <seealso cref="ShipmentDto"/></returns> 
        Task<ShipmentDto> GetShipmentByTrackingNumberAsync(string trackingnumber, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get shipment by pick up date
        /// </summary>
        /// <param name="pickupdatetime">the pickup datetime of the shipment that we want to get
        /// <returns>A <see cref="Task"/> that contains <seealso cref="ShipmentDto"/></returns>
        Task<ShipmentDto> GetShipmentByPickUpDateTimeAsync(DateTimeOffset pickupdatetime, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get shipment by actual delivery date
        /// </summary>
        /// <param name="actualdeliverydate">the actual delivery date of the shipment that we want to get
        /// <returns>A <see cref="Task"/> that contains <seealso cref="ShipmentDto"/></returns>
        Task<ShipmentDto> GetShipmentByActualDeliveryDateAsync(DateTimeOffset actualdeliverydate, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get shipment by cost
        /// </summary>
        /// <param name="cost">the cost of the shipment that we want to get
        /// <returns>A <see cref="Task"/>that contains <seealso cref="ShipmentDto"/></returns>
        Task<ShipmentDto> GetShipmentByCostAsync(decimal cost, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get shipment by status
        /// </summary>
        /// <param name="status">the status of the shipment that we want to get 
        /// <returns>A <see cref="Task"/>that contains <seealso cref="ShipmentDto"/> </returns>
        Task<ShipmentDto> GetShipmentByStatusAsync(string status, CancellationToken cancellationToken); 
    }
}
