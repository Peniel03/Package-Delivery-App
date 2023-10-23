using ShipmentService.DataAccess.Models;

namespace ShipmentService.DataAccess.Interfaces
{
    /// <summary>
    /// The location repository interface to perfom additionals operations on Location 
    /// </summary>
    public interface ILocationRepository: IBaseRepository<Location>
    {
        /// <summary>
        /// Function to get the location by address
        /// </summary>
        /// <param name="address">the address from where we want to get the location </param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="Location"/></returns>
        Task<Location> GetLocationByAddress(string address, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get the location by city 
        /// </summary>
        /// <param name="city">the city from where we want to get the location </param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="Location"/></returns>
        Task<Location> GetLocationByCity(string city, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get the location by country 
        /// </summary>
        /// <param name="country">the country from where we want to get the location</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="Location"/></returns>
        Task<Location> GetLocationByCountry(string country, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get the location by postalcode 
        /// </summary>
        /// <param name="postalCode">the postalcode from where we want to get the location</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="Location"/></returns>
        Task<Location> GetLocationByPostalCode(string postalCode, CancellationToken cancellationToken); 


    }
}
