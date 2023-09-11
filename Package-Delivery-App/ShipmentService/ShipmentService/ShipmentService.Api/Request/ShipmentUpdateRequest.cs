namespace ShipmentService.Api.Request
{
    /// <summary>
    /// the shipment for the update request
    /// </summary>
    public class ShipmentUpdateRequest
    {
        /// <summary>
        /// the catual delivery date
        /// </summary>
        public string DeliveryMethod { get; set; } = string.Empty;

        /// <summary>
        /// the shipment estimated delivery datetime
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
