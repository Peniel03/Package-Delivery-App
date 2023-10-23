using ExpeditionService.DataAccess.DataContext;
using ExpeditionService.DataAccess.Interfaces;
using ExpeditionService.DataAccess.Models;
using MongoFramework;
using MongoFramework.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionService.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of the repository for crud and additionals operations on the location table
    /// </summary>
    public class LocationRepository : ILocationRepository
    {
        private readonly ExpeditionContext _expeditionContext;
        private readonly MongoDbSet<Location> _location; 

        /// <summary>
        /// initialization of a new instance of <see cref="PackageRepository"/>
        /// </summary>
        /// <param name="shipmentContext">the database context</param>
        public LocationRepository(ExpeditionContext expeditionContext)
        {
            _expeditionContext = expeditionContext;
            _location = (MongoDbSet<Location>?)_expeditionContext.Set<Location>(); 
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="location">the location that we want to add</param>
        public void Add(Location location)
        {
            _location.Add(location);
         }

        // <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="location">the location that we want to delete</param>
        public void Delete(Location location)
        {
            _location.Remove(location);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/> that contains a list of <seealso cref="Location"/></returns>
        public Task<List<Location>> GetAll(CancellationToken cancellationToken)
        {
            return _location
                        .AsNoTracking()
                        .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="id">the id of the location</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="Location"/></returns>
        public Task<Location> GetById(string id, CancellationToken cancellationToken)
        {
            return _location
                      .AsNoTracking()
                      .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// <inheritdoc/> 
        /// </summary>
        /// <param name="address">the adress of the location</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="Location"/></returns>
        public Task<Location> GetLocationByAddress(string address, CancellationToken cancellationToken)
        {
            return _location
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Address == address);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="city">the city of the location</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="Location"/></returns>
        public Task<Location> GetLocationByCity(string city, CancellationToken cancellationToken)
        {
            return _location
                     .AsNoTracking()
                     .FirstOrDefaultAsync(x => x.City == city);  
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="country">the contry of the location</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="Location"/></returns>
        public Task<Location> GetLocationByCountry(string country, CancellationToken cancellationToken)
        {
            return _location
                         .AsNoTracking()
                         .FirstOrDefaultAsync(x => x.Country == country); 
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="postalCode">the postal code of the location</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns> A <see cref="Task"/>That contains <seealso cref="Location"/></returns>
        public Task<Location> GetLocationByPostalCode(string postalCode, CancellationToken cancellationToken)
        {
            return _location
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.PostalCode == postalCode); 
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="location">the location that we want to update</param>
        public void Update(Location location)
        {
            _location.Update(location);
        }
    }
}
