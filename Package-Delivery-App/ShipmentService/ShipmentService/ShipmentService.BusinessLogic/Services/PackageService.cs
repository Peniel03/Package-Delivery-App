using AutoMapper;
using ShipmentService.BusinessLogic.DTOs;
using ShipmentService.BusinessLogic.Exceptions;
using ShipmentService.BusinessLogic.Interfaces;
using ShipmentService.DataAccess.Interfaces;
using ShipmentService.DataAccess.Models;


namespace ShipmentService.BusinessLogic.Services
{
    /// <summary>
    /// the implementation of package service to manage packages
    /// </summary>
    public class PackageService : IPackageService
    {
        private readonly IPackageRepository _packageRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly ISaveChangesRepository _saveChangesRepository;

        /// <summary>
        ///  initialization of a new instance of <see cref="PackageService"/>
        /// </summary>
        /// <param name="packageRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="saveChangesRepository"></param>
        public PackageService(IPackageRepository packageRepository,
            IMapper mapper,
            ILoggerManager logger,
            ISaveChangesRepository saveChangesRepository)
        {
            _packageRepository = packageRepository;
            _mapper = mapper;
            _logger = logger;
            _saveChangesRepository = saveChangesRepository;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="packageDto">the package's data transfer object</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="ArgumentException">the argument exception</exception>
        /// <returns>A <see cref="Task"/>That contains <seealso cref="PackageDto"/></returns>
        public async Task<PackageDto> AddAsync(PackageDto packageDto, CancellationToken cancellationToken)
        {
            var mappedPackage = _mapper.Map<Package>(packageDto);
            var checkedPackage = await _packageRepository.GetById(mappedPackage.Id, cancellationToken);

            if (checkedPackage != null)
            {
                _logger.LogError("Error occured while adding the package");
                throw new AlreadyExistException("This package already exist");
            }

            _packageRepository.Add(mappedPackage);

            try
            {
                await _saveChangesRepository.SaveChangesAsync();
                _logger.LogInfo("Changes successfully saved in the database");
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error occured while adding a package{ex.Message}");
                throw new ArgumentException($"Something went wrong while adding the package {ex.Message}");
            }
            return packageDto; 
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="packageDto">the package's datat transfert object</param>
        /// <param name="cancellationToken">the cancelation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <exception cref="ArgumentException">the argument exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="PackageDto"/></returns>

        public async Task<PackageDto> DeleteAsync(PackageDto packageDto, CancellationToken cancellationToken)
        {
            var mappedPackage = _mapper.Map<Package>(packageDto);
            var checkedPackage = await _packageRepository.GetById(mappedPackage.Id, cancellationToken);
            if (checkedPackage == null)
            {
                throw new NotFoundException("The package wasn't found");
            }
            try
            {
                _packageRepository.Delete(mappedPackage);
                await _saveChangesRepository.SaveChangesAsync();
                _logger.LogInfo("Changes successfully saved in the database");
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Something went wrong while deleting the package {ex.Message}", ex);
            }
            return packageDto;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="PackageDto"/></returns>
        public async Task<List<PackageDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var list = await _packageRepository.GetAll(cancellationToken);
            if (list == null)
            {
                throw new NotFoundException("There no registered packages yet");
            }

            var listDto = _mapper.Map<List<PackageDto>>(list);

            return listDto;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="packageDto">the package datat transfert object</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="PackageDto"/></returns>
        public async Task<PackageDto> GetByIdAsync(PackageDto packageDto, CancellationToken cancellationToken)
        {
            var mappedPackage = _mapper.Map<Package>(packageDto);
            var checkedPackage = await _packageRepository.GetById(mappedPackage.Id, cancellationToken);
            if (checkedPackage == null)
            {
                throw new NotFoundException("This package wasn't found");
            }
            return _mapper.Map<PackageDto>(checkedPackage); 
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="packageDto">the package's data transfer object</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="NotImplementedException"></exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="PackageDto"/></returns>
        public async Task<PackageDto> GetPackageByDimensionsAsync(PackageDto packageDto, CancellationToken cancellationToken)
        {
            var mappedPackage = _mapper.Map<Package>(packageDto);
            var checkedPackage = await _packageRepository.GetPackageByDimensions(mappedPackage.Dimensions, cancellationToken);
            if (checkedPackage == null)
            {
                throw new NotFoundException("This package wasn't found");
            }
            return _mapper.Map<PackageDto>(checkedPackage);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="packageDto">the package's data transfer object</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="PackageDto"/></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<PackageDto> GetPackageByOwnerIdAsync(PackageDto packageDto, CancellationToken cancellationToken)
        {
            var mappedPackage = _mapper.Map<Package>(packageDto);
            var checkedPackage = await _packageRepository.GetPackageByOwnerId(mappedPackage.OwnerId, cancellationToken);
            if (checkedPackage == null)
            {
                throw new NotFoundException("This package wasn't found");
            }
            return _mapper.Map<PackageDto>(checkedPackage);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="packageDto">the package data transfert object</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="PackageDto"/></returns>
        /// <exception cref="NotFoundException"></exception>
        public async Task<PackageDto> GetPackageByWeightAsync(PackageDto packageDto, CancellationToken cancellationToken)
        {
            var mappedPackage = _mapper.Map<Package>(packageDto);
            var checkedPackage = await _packageRepository.GetPackageByWeight(mappedPackage.Weight, cancellationToken);
            if (checkedPackage == null)
            {
                throw new NotFoundException("This package wasn't found");
            }
            return _mapper.Map<PackageDto>(checkedPackage);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="packageDto">the package data transfer object</param>
        /// <param name="cancellationToken">the cancelation token</param>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>A <see cref="Task"/>That contains <seealso cref="PackageDto"/></returns>

        public async Task<PackageDto> UpdateAsync(PackageDto packageDto, CancellationToken cancellationToken)
        {
            var mappedPackage = _mapper.Map<Package>(packageDto);
            var checkedPackage = await _packageRepository.GetById(mappedPackage.Id, cancellationToken);
            if (checkedPackage == null)
            {
                throw new NotFoundException("This package wasn't found");

            }
            try
            {
                _packageRepository.Update(mappedPackage);
                await _saveChangesRepository.SaveChangesAsync();
                _logger.LogInfo("Changes successfully saved in the database");
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Something went wrong while adding the package {ex.Message}");
            }

            return packageDto; 
        }      
    }
}
