namespace ShipmentService.Api.Request
{
    /// <summary>
    /// the request for the package
    /// </summary>
    public class PackageRequest
    {
         
        /// <summary>
        /// the weight of the package
        /// </summary>
        public decimal Weight { get; set; }

        /// <summary>
        /// the dimensions of the package
        /// </summary>
        public string Dimensions { get; set; } = string.Empty;
                         
        /// <summary>
        /// the description of the package and it content 
        /// </summary>
        public string ContentDescription { get; set; } = string.Empty;

    }
}
