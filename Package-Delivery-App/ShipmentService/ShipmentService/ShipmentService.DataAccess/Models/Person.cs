

namespace ShipmentService.DataAccess.Models
{
    /// <summary>
    /// The Person who  will receive or send the package
    /// </summary>
    public  class Person
    {
        /// <summary>
        /// The Id of the person
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// The name of the person
        /// </summary>
        public string Name { get; set; } = string.Empty;

        /// <summary>
        /// the phone number of the person 
        /// </summary>
        public string Phone { get; set; }= string.Empty;

        /// <summary>
        /// the Id of the person's location 
        /// </summary>
        public int LocationId { get; set; }

        /// <summary>
        /// the location of the person 
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// the bavigation property for shipment
        /// </summary>
        public List<Shipment> shipments { get; set; }

        /// <summary>
        /// the navigation property for package
        /// </summary>
        public List<Package> Packages { get; set; }
    }
}
