using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionService.DataAccess.Models
{
    public class Package
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        /// <summary>
        /// The id of the package
        /// </summary>
        public int Id { get; set; }
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
