using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace ExpeditionService.DataAccess.Models
{
    /// <summary>
    /// the llocation class
    /// </summary>
    public class Location
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        /// <summary>
        /// The Location id
        /// </summary>
        public string Id { get; set; } 

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
