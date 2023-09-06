namespace ShipmentService.Api.Request
{
    /// <summary>
    /// The request fo the perosn
    /// </summary>
    public class PersonRequest
    {
        /// <summary>
        /// The name of the person
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// the phone number of the person 
        /// </summary>
        public string Phone { get; set; } = string.Empty;

    }
}
