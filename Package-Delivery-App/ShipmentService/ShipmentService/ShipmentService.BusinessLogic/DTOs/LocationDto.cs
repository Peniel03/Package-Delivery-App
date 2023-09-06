using ShipmentService.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.BusinessLogic.DTOs
{
    /// <summary>
    /// the location's data transfer object
    /// </summary>
    public class LocationDto
    {
        /// <summary>
        /// The Location id
        /// </summary>
        public int Id { get; set; }

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

        /// <summary>
        /// navigation property for Person
        /// </summary>
        public List<Person> Persons { get; set; }

        /// <summary>
        /// navigation property for Shipment
        /// </summary>
        public List<Shipment> Shipements { get; set; }

    }
}
