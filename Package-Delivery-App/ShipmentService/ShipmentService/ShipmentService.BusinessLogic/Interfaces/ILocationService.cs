using ShipmentService.BusinessLogic.DTOs;

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
        /// <param name="address">the address of the location that we want to get
        /// <returns>A <see cref="Task"/> that contains <seealso cref="LocationDto"/></returns>
        Task<LocationDto> GetLocationByAddressAsync(string address, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get the location by city 
        /// </summary>
        /// <param name="city">the entity of the location that we want to get 
        /// <returns>A <see cref="Task"/> that contains <seealso cref="LocationDto"/></returns>
        Task<LocationDto> GetLocationByCityAsync(string city, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get the location by country 
        /// </summary>
        /// <param name="country">the entity that will help us to access the country in the record 
        /// <returns>A <see cref="Task"/> that contains <seealso cref="LocationDto"/></returns>
        Task<LocationDto> GetLocationByCountryAsync(string country, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get the location by postalcode 
        /// </summary>
        /// <param name="postalcode">the postalcode of the entity that we want to get  
        /// <returns>A <see cref="Task"/> that contains <seealso cref="LocationDto"/></returns>
        Task<LocationDto> GetLocationByPostalCodeAsync(string postalcode , CancellationToken cancellationToken);
    }
}
