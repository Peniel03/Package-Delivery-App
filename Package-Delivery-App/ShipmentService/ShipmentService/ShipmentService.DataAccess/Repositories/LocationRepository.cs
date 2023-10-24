using Microsoft.EntityFrameworkCore;
using ShipmentService.DataAccess.DataContext;
using ShipmentService.DataAccess.Interfaces;
using ShipmentService.DataAccess.Models;
using System.Linq.Expressions;

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
        public void AddAsync(Location location)
        {
            _locations.Add(location);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="location">the location that we want to delete</param>
        public void DeleteAsync(Location location)
        {
            _locations.Remove(location);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/> that contains a list of <seealso cref="Location"/></returns>
        public async Task<List<Location>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _locations
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Location> GetBySomethingAsync(Func<Location, bool> predicate, CancellationToken cancellationToken)
        {
            var query = _locations.AsQueryable();
            foreach (var propertyInfo in typeof(Location).GetProperties())
            {
                var parameter = Expression.Parameter(typeof(Location), "x");
                var propertyAccess = Expression.Property(parameter, propertyInfo);
                var value = Expression.Constant(propertyInfo.GetValue(predicate.Target));
                var condition = Expression.Equal(propertyAccess, value);
                var lambda = Expression.Lambda<Func<Location, bool>>(condition, parameter);
                query = query.Where(lambda);
            } 
            return await query
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken);
        } 

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="location">the location that we want to update</param>
        public void UpdateAsync(Location location)
        {
            _locations.Update(location);
        }
    }
}
