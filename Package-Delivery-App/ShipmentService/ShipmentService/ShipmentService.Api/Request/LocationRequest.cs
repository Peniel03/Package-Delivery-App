namespace ShipmentService.Api.Request
{
    /// <summary>
    /// the request for location
    /// </summary>
    public class LocationRequest
    {

        /// <summary>
        /// The location name 
        /// </summary>
        public string LocationName { get; set; } = string.Empty;

        /// <summary>
        /// The adress of the location
        /// </summary>
        public string Address { get; set; } = string.Empty;

        /// <summary>
        /// the city of the location
        /// </summary>
        public string City { get; set; } = string.Empty;

        /// <summary>
        /// the country of the location
        /// </summary>
        public string Country { get; set; } = string.Empty;

        /// <summary>
        /// the postal code of the location 
        /// </summary>
        public string PostalCode { get; set; } = string.Empty;
    }
}
