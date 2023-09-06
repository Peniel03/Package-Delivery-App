using ShipmentService.BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.BusinessLogic.Interfaces
{
    /// <summary>
    /// the location service interface to perform additionals operations on location
    /// </summary>
    public interface ILocationService: IBaseService<LocationDto>
    {
        /// <summary>
        /// Function to get the location by address
        /// </summary>
        /// <param name="locationDto">the entity that will help us to access the address in the record
        /// where we want to get the location from</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="LocationDto"/></returns>
        Task<LocationDto> GetLocationByAddressAsync(LocationDto locationDto, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get the location by city 
        /// </summary>
        /// <param name="locationDto">the entity that will help us to access the city in the record 
        /// where we want to get the location from </param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="LocationDto"/></returns>
        Task<LocationDto> GetLocationByCityAsync(LocationDto locationDto, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get the location by country 
        /// </summary>
        /// <param name="locationDto">the entity that will help us to access the country in the record 
        /// where we want to get the location from</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="LocationDto"/></returns>
        Task<LocationDto> GetLocationByCountryAsync(LocationDto locationDto, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get the location by postalcode 
        /// </summary>
        /// <param name="locationDto">the entity that will help us to access the postal code in the record 
        /// where we want to get the location from</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="LocationDto"/></returns>
        Task<LocationDto> GetLocationByPostalCodeAsync(LocationDto locationDto, CancellationToken cancellationToken);

    }
}
