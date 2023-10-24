using ExpeditionService.DataAccess.DataContext;
using ExpeditionService.DataAccess.Interfaces;
using ExpeditionService.DataAccess.Models;
using MongoFramework;
using MongoFramework.Linq;
using System.Linq.Expressions;

namespace ExpeditionService.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of the repository for crud and additionals operations on the shipment table
    /// </summary>
    public class ShipmentRepository : IShipmentRepository
    {
        private readonly ExpeditionContext _expeditionContext;
        private readonly MongoDbSet<Shipment> _shipments;

        /// <summary>
        /// initialization of a new instance of <see cref="ShipmentRepository"/>
        /// </summary>
        /// <param name="ExpeditionContext"></param>
        public ShipmentRepository(ExpeditionContext expeditionContext)
        {
            _expeditionContext = expeditionContext;
            _shipments = (MongoDbSet<Shipment>?)_expeditionContext.Set<Shipment>();  
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="shipment"></param>
        public void AddAsync(Shipment shipment)
        {
            _shipments.Add(shipment);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="shipment"></param>
        public void DeleteAsync(Shipment shipment)
        {
            _shipments.Remove(shipment);  
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task"/> that contains a list of <seealso cref="Shipment"/></returns>
        public async Task<List<Shipment>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _shipments
                       .AsNoTracking()
                       .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Shipment> GetBySomethingAsync(Func<Shipment, bool> predicate, CancellationToken cancellationToken)
        { 
            var query = _shipments.AsQueryable();
            foreach (var propertyInfo in typeof(Shipment).GetProperties())
            {
                var parameter = Expression.Parameter(typeof(Shipment), "x");
                var propertyAccess = Expression.Property(parameter, propertyInfo);
                var value = Expression.Constant(propertyInfo.GetValue(predicate.Target));
                var condition = Expression.Equal(propertyAccess, value);
                var lambda = Expression.Lambda<Func<Shipment, bool>>(condition, parameter);
                query = query.Where(lambda); 
            }
            return await query
              //  .AsNoTracking
                .FirstOrDefaultAsync(cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="shipment"></param>
        public void UpdateAsync(Shipment shipment)
        {
            _shipments.Update(shipment); 
         }
    }
}
