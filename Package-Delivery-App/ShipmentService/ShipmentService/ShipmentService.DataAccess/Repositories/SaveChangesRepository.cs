using ShipmentService.DataAccess.DataContext;
using ShipmentService.DataAccess.Interfaces;

namespace ShipmentService.DataAccess.Repositories
{
    /// <summary>
    /// implementation of the save changes repository
    /// </summary>
    public class SaveChangesRepository : ISaveChangesRepository
    {
        private readonly ShipmentContext _shipmentContext;

        /// <summary>
        /// initialization of a new instance of <see cref="SaveChangesRepository"/>
        /// </summary>
        /// <param name="shipmentContext">the database context</param>
        public SaveChangesRepository(ShipmentContext shipmentContext)
        {
            _shipmentContext = shipmentContext;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns> A <see cref="Task"/></returns>
        public Task SaveChangesAsync()
        {
            return _shipmentContext.SaveChangesAsync();
        }
    }
}
