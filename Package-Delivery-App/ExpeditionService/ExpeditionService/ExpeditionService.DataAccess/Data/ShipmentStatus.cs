namespace ExpeditionService.DataAccess.Data
{
    /// <summary>
    /// Enum class to set values for the shipment status property
    /// </summary>
    public enum ShipmentStatus
    {
        Received ,
        InProcess,
        Shipped,
        InTransit,
        Arrived,
        ReadyForPickUp 
    }
} 
