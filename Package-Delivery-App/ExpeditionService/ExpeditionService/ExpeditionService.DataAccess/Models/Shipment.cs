using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionService.DataAccess.Models
{
    /// <summary>
    ///  The shipment class
    /// </summary>
    public class Shipment
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        /// <summary>
        /// the shipment id
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// the shipment tracking number
        /// </summary>
        public string TrackingNumber { get; set; } = string.Empty;

        /// <summary>
        /// the shipment pickupdatetime
        /// </summary>
        public DateTimeOffset PickupDateTime { get; set; }

        /// <summary>
        /// the shipment delivery method
        /// </summary>
        public string DeliveryMethod { get; set; } = string.Empty;

        /// <summary>
        /// the shipment estimated delivery datetime
        /// </summary>
        public DateTimeOffset EstimatedDeliveryDateTime { get; set; }

        /// <summary>
        /// the shipment actual delivery datetime
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

        /// <summary>
        /// the pick up location id
        /// </summary>
        public int PickUpLocationId { get; set; }

        /// <summary>
        /// the destination location id
        /// </summary>
        public int DestinationLocationId { get; set; }

        
        /// <summary>
        /// the navigation property for location
        /// </summary>
        public Location Location { get; set; }

        /// <summary>
        /// the package id
        /// </summary>
        public int PackageId { get; set; }

        /// <summary>
        /// The navigation property for package
        /// </summary>
        public Package Package { get; set; }

        /// <summary>
        /// the sender Id
        /// </summary>
        public int SenderId { get; set; }

        /// <summary>
        /// the recipient id
        /// </summary>
        public int RecipientId { get; set; }

        /// <summary>
        /// The navigation property for person
        /// </summary>
        public Person Person { get; set; }

        /// <summary>
        /// the id of the user or employee that will register the shipment
        /// </summary>
        public int UserId { get; set; }


    }
}
