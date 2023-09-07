using MongoDB.Bson.Serialization.Conventions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionService.BusinessLogic.Helpers
{
    /// <summary>
    /// ShipmentHelperFunctions class to perform operation according to business needs
    /// </summary>
    public static class ShipmentHelperFunctions
    {
        /// <summary>
        /// Method to set the estimated delivery date.
        /// </summary>
        /// <param name="pickUpDateTime">the pick up date time</param>
        /// <returns>A <see cref="DateTimeOffset"/>that contains the estimated delivery date</returns>
        public static DateTimeOffset SetEstimatedDeliveryDate(DateTimeOffset pickUpDateTime) 
        {
            DateTimeOffset estimatedDeliveryDate = pickUpDateTime.AddDays(7);   
            return estimatedDeliveryDate;
        }

        /// <summary>
        /// method to  set the shipment cost 
        /// </summary>
        /// <param name="packageWeight">the weight of the package</param>
        /// <returns>A <see cref="decimal"/>that contains the cost of the shipment</returns> 
        public static decimal SetShipmentCost(decimal packageWeight)
        {
            //for 1Kg the fees are = 10$
            decimal cost = (packageWeight * 10);
            return cost;
        }

    }
}
