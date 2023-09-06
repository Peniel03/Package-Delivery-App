using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShipmentService.DataAccess.DataContext;
using ShipmentService.DataAccess.Interfaces;
using ShipmentService.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of the repository for crud and additionals operations on the package table
    /// </summary>
    public class PackageRepository : IPackageRepository
    {
        private readonly ShipmentContext _shipmentContext;
        private readonly DbSet<Package> _packages;
        private readonly ILogger<Package> _logger;

        /// <summary>
        /// initialization of a new instance of <see cref="PackageRepository"/>
        /// </summary>
        /// <param name="shipmentContext">the database context</param>
        /// <param name="logger">the logger </param>
        public PackageRepository(ShipmentContext shipmentContext, ILogger<Package> logger)
        {
            _shipmentContext = shipmentContext;
            _packages = _shipmentContext.Set<Package>();
            _logger = logger;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="package">the package that we want to add</param>
        public void Add(Package package)
        {
            _packages.Add(package);
            _logger.LogInformation($"The package {package} has been added to the database");
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="package">the package that we want to delete</param>
        public void Delete(Package package)
        {
            _packages.Add(package);
            _logger.LogInformation($"The package {package} has been deleted from the database");
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>That contains a List of <seealso cref="Package"/></returns>
        public Task<List<Package>> GetAll(CancellationToken cancellationToken)
        {
            return _packages
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

       /// <summary>
       /// <inheritdoc/>
       /// </summary>
       /// <param name="id">the id of the package</param>
       /// <param name="cancellationToken">the cancellation token</param>
       /// <returns>A <see cref="Task"/>that contains a <seealso cref="Package"/></returns>
        public Task<Package> GetById(int id, CancellationToken cancellationToken)
        {
            return _packages
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id , cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="dimensions">the dimensions of the package</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>that contains a <seealso cref="Package"/></returns>
        public Task<Package> GetPackageByDimensions(string dimensions, CancellationToken cancellationToken)
        {
            return _packages
                 .AsNoTracking()
                 .FirstOrDefaultAsync(x => x.Dimensions == dimensions, cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="ownerId">the id of the owner of the package</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>that contain <seealso cref="Package"/></returns>
        public Task<Package> GetPackageByOwnerId(int ownerId, CancellationToken cancellationToken)
        {
            return _packages
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.OwnerId == ownerId, cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="Package"/></returns>

        public Task<Package> GetPackageByWeight(decimal weight, CancellationToken cancellationToken)
        {
            return _packages
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Weight == weight, cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="package">the package that we want to update</param>
        public void Update(Package package)
        {
            _packages.Update(package);
            _logger.LogInformation($"the package {package} has been updated  ");
        }

    }
}
