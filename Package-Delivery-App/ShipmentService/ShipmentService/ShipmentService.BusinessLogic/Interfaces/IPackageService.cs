using ShipmentService.BusinessLogic.DTOs;

namespace ShipmentService.BusinessLogic.Interfaces
{
    /// <summary>
    /// The package service interface to perfom additionals operations on Package
    /// </summary>
    public interface IPackageService: IBaseService<PackageDto>
    {
        /// <summary>
        /// Function to get the package by weight
        /// </summary>
        /// <param name="packageDto">the entity that will help us to access the weight in the record 
        /// where we want to get the package from </param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="PackageDto"/></returns>
        Task<PackageDto> GetPackageByWeightAsync(PackageDto packageDto, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get the package by dimensions
        /// </summary>
        /// <param name="packageDto">the entity that will help us to access the dimensions in the record 
        /// where we want to get the package from</param>
        /// <returns>A <see cref="Task"/>that contains a <seealso cref="PackageDto"/></returns>
        Task<PackageDto> GetPackageByDimensionsAsync(PackageDto packageDto, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get the package by ownerid
        /// </summary>
        /// <param name="packageDto">the entity that will help us to access the ownerId in the record 
        /// where we want to get the package from</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="PackageDto"/></returns>
        Task<PackageDto> GetPackageByOwnerIdAsync(PackageDto packageDto, CancellationToken cancellationToken);

    }
}
