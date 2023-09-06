using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShipmentService.DataAccess.DataContext;
using ShipmentService.DataAccess.Interfaces;
using ShipmentService.DataAccess.Models;


namespace ShipmentService.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of the repository for crud and additionals operations on the shipment table
    /// </summary>
    public class ShipmentRepository: IShipmentRepository
    {
        private readonly ShipmentContext _shipmentContext;
        private readonly DbSet<Shipment> _shipments;
        private readonly ILogger<Shipment> _logger;

        /// <summary>
        /// initialization of a new instance of <see cref="ShipmentRepository"/>
        /// </summary>
        /// <param name="shipmentContext"></param>
        /// <param name="logger"></param>
        public ShipmentRepository(ShipmentContext shipmentContext, ILogger<Shipment> logger)
        {
            _shipmentContext = shipmentContext;
            _shipments = _shipmentContext.Set<Shipment>();
            _logger = logger;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="shipment"></param>
        public void Add(Shipment shipment)
        {
            _shipments.Add(shipment);
            _logger.LogInformation($"the shipment {shipment.TrackingNumber} has been added to the database");
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
        public Task<Shipment> GetById(int id, CancellationToken cancellationToken)
        {
            return _shipments
                           .AsNoTracking()
                           .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="actualDeliveryDateTime"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task"/>That contains <seealso cref="Location"/></returns>
        public Task<Shipment> GetShipmentByActualDeliveryDate(DateTimeOffset actualDeliveryDateTime, CancellationToken cancellationToken)
        {
            return _shipments
                      .AsNoTracking()
                       .FirstOrDefaultAsync(x => x.ActualDeliveryDateTime == actualDeliveryDateTime, cancellationToken);
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
                         .FirstOrDefaultAsync(x => x.ShipmentCost == cost, cancellationToken);
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
                       .FirstOrDefaultAsync(x => x.PickupDateTime == pickUpDateTime, cancellationToken);
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
                        .FirstOrDefaultAsync(x => x.ShipmentStatus == status, cancellationToken);
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
                        .FirstOrDefaultAsync(x => x.TrackingNumber == trackingNumber, cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="shipment"></param>
        public void Update(Shipment shipment)
        {
            _shipments.Update(shipment);
            _logger.LogInformation($"The shipment {shipment.TrackingNumber} has been updated");
         }
    }
}
