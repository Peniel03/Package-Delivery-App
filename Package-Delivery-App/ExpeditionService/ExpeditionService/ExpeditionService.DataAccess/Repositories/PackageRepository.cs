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
    /// Implementation of the repository for crud and additionals operations on the package table
    /// </summary>
    public class PackageRepository: IPackageRepository
    {
        private readonly ExpeditionContext _expeditionContext;
        private readonly MongoDbSet<Package> _package;

        /// <summary>
        /// initialization of a new instance of <see cref="PackageRepository"/>
        /// </summary>
        /// <param name="shipmentContext">the database context</param>
        public PackageRepository(ExpeditionContext expeditionContext)
        {
            _expeditionContext = expeditionContext;
            _package = (MongoDbSet<Package>?)_expeditionContext.Set<Package>(); 
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="package">the package that we want to add</param>
        public void Add(Package package) 
        {
            _package.Add(package);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="package">the package that we want to delete</param>
        public void Delete(Package package)
        {
            _package.Remove(package);
         }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>That contains a List of <seealso cref="Package"/></returns>
        public Task<List<Package>> GetAll(CancellationToken cancellationToken)
        {
            return _package
                         .AsNoTracking()
                          .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="id">the id of the package</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>that contains a <seealso cref="Package"/></returns>
        public Task<Package> GetById(string id, CancellationToken cancellationToken)
        {
            return _package
                          .AsNoTracking()
                          .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="dimensions">the dimensions of the package</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>that contains a <seealso cref="Package"/></returns>
        public Task<Package> GetPackageByDimensions(string dimensions, CancellationToken cancellationToken)
        {
            return _package
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.Dimensions == dimensions);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="ownerId">the id of the owner of the package</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>that contain <seealso cref="Package"/></returns>
        public Task<Package> GetPackageByOwnerId(int ownerId, CancellationToken cancellationToken)
        {
            return _package
                        .AsNoTracking()
                        .FirstOrDefaultAsync(x => x.OwnerId == ownerId);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="weight"></param>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="Package"/></returns>
        public Task<Package> GetPackageByWeight(decimal weight, CancellationToken cancellationToken)
        {
            return _package
                           .AsNoTracking()
                           .FirstOrDefaultAsync(x => x.Weight == weight);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="package">the package that we want to update</param>
        public void Update(Package package) 
        {
            _package.Update(package);
        }
    }
}
