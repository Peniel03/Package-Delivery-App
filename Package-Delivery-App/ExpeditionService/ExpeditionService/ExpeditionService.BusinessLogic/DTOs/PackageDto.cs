using ExpeditionService.DataAccess.Models;

namespace ExpeditionService.BusinessLogic.DTOs
{
    /// <summary>
    /// the package's data transfer object 
    /// </summary>
    public class PackageDto
    {
        /// <summary>
        /// The id of the package
        /// </summary>
        public string Id { get; set; }
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
        /// <summary>
        /// the package owner's Id 
        /// </summary>
        public int OwnerId { get; set; }

        /// <summary>
        /// navigation property for person 
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// navigation property for shipment
        /// </summary>
        public Shipment Shipment { get; set; }
    }
}
