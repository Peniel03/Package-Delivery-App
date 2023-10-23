using ExpeditionService.DataAccess.DataContext;
using ExpeditionService.DataAccess.Interfaces;
using ExpeditionService.DataAccess.Models;
using MongoFramework;
using MongoFramework.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionService.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of the repository for crud and additionals operations on the shipment table
    /// </summary>
    public class ShipmentRepository: IShipmentRepository
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
        public void Add(Shipment shipment)
        {
            _shipments.Add(shipment);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="shipment"></param>
        public void Delete(Shipment shipment)
        {
            _shipments.Remove(shipment);  
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task"/> that contains a list of <seealso cref="Shipment"/></returns>
        public Task<List<Shipment>> GetAll(CancellationToken cancellationToken)
        {
            return _shipments
                       .AsNoTracking()
                       .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="id"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task"/>That contains <seealso cref="Shipment"/></returns>
        public Task<Shipment> GetById(string id, CancellationToken cancellationToken)
        {
            return _shipments
                 .AsNoTracking()
                 .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="actualDeliveryDateTime"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task"/>That contains <seealso cref="Shipment"/></returns>
        public Task<Shipment> GetShipmentByActualDeliveryDate(DateTimeOffset actualDeliveryDateTime, CancellationToken cancellationToken)
        {
            return _shipments
                      .AsNoTracking()
                      .FirstOrDefaultAsync(x => x.ActualDeliveryDateTime == actualDeliveryDateTime); 
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="cost"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="Shipment"/></returns>
        public Task<Shipment> GetShipmentByCost(decimal cost, CancellationToken cancellationToken)
        {
            return _shipments
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.ShipmentCost == cost);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="pickUpDateTime"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task"/>that contains a list of <seealso cref="Shipment"/></returns> 
        public Task<Shipment> GetShipmentByPickUpDateTime(DateTimeOffset pickUpDateTime, CancellationToken cancellationToken)
        {
            return _shipments
                      .AsNoTracking()
                      .FirstOrDefaultAsync(x => x.PickupDateTime == pickUpDateTime);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="status"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="Shipment"/></returns>
        public Task<Shipment> GetShipmentByStatus(string status, CancellationToken cancellationToken)
        {
            return _shipments
                       .AsNoTracking()
                       .FirstOrDefaultAsync(x => x.ShipmentStatus == status); 
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="trackingNumber"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="Shipment"/></returns>
        public Task<Shipment> GetShipmentByTrackingNumber(string trackingNumber, CancellationToken cancellationToken)
        {
            return _shipments
                        .AsNoTracking()
                       .FirstOrDefaultAsync(x => x.TrackingNumber == trackingNumber); 
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="shipment"></param>
        public void Update(Shipment shipment)
        {
            _shipments.Update(shipment);
         }
    }
}
