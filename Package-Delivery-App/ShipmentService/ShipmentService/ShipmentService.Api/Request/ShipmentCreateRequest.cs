namespace ShipmentService.Api.Request
{
    /// <summary>
    ///   the shipment for the create request 
    /// </summary>
    public class ShipmentCreateRequest
    {

        /// <summary>
        /// the shipment pickupdatetime
        /// </summary>
        public DateTimeOffset PickupDateTime { get; set; }

        /// <summary>
        /// the shipment delivery method
        /// </summary>
        public string DeliveryMethod { get; set; } = string.Empty;

        /// <summary>
        /// the shipment estimated delivery datetime
        /// </summary>
        public DateTimeOffset EstimatedDeliveryDateTime { get; set; }

        /// <summary>
        /// the shipment actual delivery datetime
        /// </summary>
        public DateTimeOffset ActualDeliveryDateTime { get; set; }

        /// <summary>
        /// the shipment cost
        /// </summary>
        public decimal ShipmentCost { get; set; }

        /// <summary>
        /// the shipment status
        /// </summary>
        public string ShipmentStatus { get; set; } = string.Empty;

    }
}
