using ShipmentService.DataAccess.Models;

namespace ShipmentService.DataAccess.Interfaces
{
    /// <summary>
    /// The package repository interface to perfom additionals operations on Package
    /// </summary>
    public interface IPackageRepository: IBaseRepository<Package>
    {
        /// <summary>
        /// Function to get the package by weight
        /// </summary>
        /// <param name="weight">the weight of the package that we want to get </param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="Package"/></returns>
        Task<Package> GetPackageByWeight(decimal weight,CancellationToken cancellationToken);

        /// <summary>
        /// Function to get the package by dimensions
        /// </summary>
        /// <param name="dimensions">the dimensions of the package that we want to get</param>
        /// <returns></returns>
        Task<Package> GetPackageByDimensions(string dimensions, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get the package by ownerid
        /// </summary>
        /// <param name="ownerId">the id of the owner of the package that we want to get</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="Package"/></returns>
        Task<Package> GetPackageByOwnerId(int ownerId,CancellationToken cancellationToken);

    }
}
