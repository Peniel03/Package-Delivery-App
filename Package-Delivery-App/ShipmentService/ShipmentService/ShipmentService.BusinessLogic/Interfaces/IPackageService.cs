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
        /// <param name="weight">the weight of the package that we want to get  
        /// <returns>A <see cref="Task"/> that contains <seealso cref="PackageDto"/></returns>
        Task<PackageDto> GetPackageByWeightAsync(decimal weight, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get the package by dimensions
        /// </summary>
        /// <param name="dimensions">the dimensions of the package that we want to get  
        /// <returns>A <see cref="Task"/>that contains a <seealso cref="PackageDto"/></returns>
        Task<PackageDto> GetPackageByDimensionsAsync(string dimensions, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get the package by ownerid
        /// </summary>
        /// <param name="ownerid">the id of the owner of the package that we want to get
        /// <returns>A <see cref="Task"/> that contains <seealso cref="PackageDto"/></returns>
        Task<PackageDto> GetPackageByOwnerIdAsync(int ownerid, CancellationToken cancellationToken); 
    }
}
