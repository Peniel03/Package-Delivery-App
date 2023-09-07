using Microsoft.EntityFrameworkCore;
using ShipmentService.DataAccess.DataContext;
using ShipmentService.DataAccess.Interfaces;
using ShipmentService.DataAccess.Models;


namespace ShipmentService.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of the repository for crud and additionals operations on the location table
    /// </summary>
    public class LocationRepository : ILocationRepository
    {
        private readonly ShipmentContext _shipmentContext;
        private readonly DbSet<Location> _locations;

        /// <summary>
        /// initialization of a new instance of <see cref="LocationRepository"/>
        /// </summary>
        /// <param name="shipmentContext">the database context</param>
        public LocationRepository(ShipmentContext shipmentContext) 
        {
            _shipmentContext = shipmentContext;
            _locations = _shipmentContext.Set<Location>(); 
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="location">the location that we want to add</param>
        public void Add(Location location)
        {
            _locations.Add(location);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="location">the location that we want to delete</param>
        public void Delete(Location location)
        {
            _locations.Remove(location);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/> that contains a list of <seealso cref="Location"/></returns>
        public Task<List<Location>> GetAll(CancellationToken cancellationToken)
        {
            return _locations
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="id">the id of the location</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="Location"/></returns>
        public Task<Location> GetById(int id, CancellationToken cancellationToken)
        {
            return _locations
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id , cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="address">the adress of the location</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="Location"/></returns>
        public Task<Location> GetLocationByAddress(string address, CancellationToken cancellationToken)
        {
            return _locations
                 .AsNoTracking()
                 .FirstOrDefaultAsync(x => x.Address == address, cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="city">the city of the location</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="Location"/></returns>
        public Task<Location> GetLocationByCity(string city, CancellationToken cancellationToken)
        {
            return _locations
              .AsNoTracking()
              .FirstOrDefaultAsync(x => x.City == city, cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="country">the contry of the location</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="Location"/></returns>
        public Task<Location> GetLocationByCountry(string country, CancellationToken cancellationToken)
        {
            return _locations
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Country == country, cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="postalCode">the postal code of the location</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns> A <see cref="Task"/>That contains <seealso cref="Location"/></returns>
        public Task<Location> GetLocationByPostalCode(string postalCode, CancellationToken cancellationToken)
        {
            return _locations
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.PostalCode == postalCode, cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="location">the location that we want to update</param>
        public void Update(Location location)
        {
            _locations.Update(location);
        }
    }
}
