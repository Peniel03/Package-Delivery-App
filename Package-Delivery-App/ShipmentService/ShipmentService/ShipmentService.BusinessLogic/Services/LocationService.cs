using AutoMapper;
using ShipmentService.BusinessLogic.DTOs;
using ShipmentService.BusinessLogic.Exceptions;
using ShipmentService.BusinessLogic.Interfaces;
using ShipmentService.DataAccess.Interfaces;
using ShipmentService.DataAccess.Models;

namespace ShipmentService.BusinessLogic.Services
{
    /// <summary>
    /// the implementation of location service to manage locations
    /// </summary>
    public class LocationService : ILocationService 
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly ISaveChangesRepository _saveChangesRepository;

        /// <summary>
        /// initialization of a new instance of <see cref="LocationService"/>
        /// </summary>
        /// <param name="locationRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="saveChangesRepository"></param>
        public LocationService(ILocationRepository locationRepository, 
            IMapper mapper,
            ILoggerManager logger, 
            ISaveChangesRepository saveChangesRepository)
        {
            _locationRepository = locationRepository;
            _mapper = mapper;
            _logger = logger;
            _saveChangesRepository = saveChangesRepository; 
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="locationDto">the location's data transfer object</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="ArgumentException">the argument exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="LocationDto"/></returns>
        public async Task<LocationDto> AddAsync(LocationDto locationDto, CancellationToken cancellationToken)
        {
            var mappedLocation = _mapper.Map<Location>(locationDto);
            var checkedLocation = await _locationRepository.GetBySomethingAsync(x => x.Id == mappedLocation.Id, cancellationToken); 
            if (checkedLocation != null)
            {
                _logger.LogError("Error occured while adding the location");
                throw new AlreadyExistException("This location already exist");
            }
            _locationRepository.AddAsync(mappedLocation);
            try
            {
                await _saveChangesRepository.SaveChangesAsync();
                _logger.LogInfo("Changes successfully saved in the database");
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error occured while adding a location{ex.Message}");  
                throw new ArgumentException($"Something went wrong while adding the location {ex.Message}");
            }
            return locationDto; 
        } 

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="locationDto">the location's data transfer object</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <exception cref="ArgumentException">the argument exception</exception>
        /// <returns>A <see cref="Task"/>that contains a <seealso cref="LocationDto"/></returns>

        public async Task<LocationDto> DeleteAsync(int id, CancellationToken cancellationToken)
        {
            LocationDto locationDto = new LocationDto(); 
            var mappedLocation = _mapper.Map<Location>(locationDto);
            mappedLocation.Id = id;
            var checkedLocation = await _locationRepository.GetBySomethingAsync(x => x.Id == mappedLocation.Id, cancellationToken); 
            if(checkedLocation == null)
            {                                   
                throw new NotFoundException("The location wasn't found");
            } 
            try
            {
                _locationRepository.DeleteAsync(mappedLocation);
                await _saveChangesRepository.SaveChangesAsync();
                _logger.LogInfo("Changes successfully saved in the database");
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Something went wrong while deleting the location {ex.Message}", ex);
            }
            return locationDto;
         }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>A <see cref="Task"/>that contains a list of <seealso cref="LocationDto"/></returns>
        public async Task<List<LocationDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var list = await _locationRepository.GetAllAsync(cancellationToken);
            if(list == null)
            {
                throw new NotFoundException("There no registered location yet");
            }
            var listDto = _mapper.Map<List<LocationDto>>(list);
            return listDto;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="id">the id of the location that we want to delete</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="LocationDto"/></returns>
        public async Task<LocationDto> GetByIdAsync(int id, CancellationToken cancellationToken)
        {
            LocationDto locationDto = new LocationDto();
            var mappedLocation = _mapper.Map<Location>(locationDto);
            mappedLocation.Id = id;
            var checkedLocation = await _locationRepository.GetBySomethingAsync( x => x.Id == mappedLocation.Id, cancellationToken); 
            if(checkedLocation == null)
            {
                throw new NotFoundException("This location wasn't found");
            }
            return _mapper.Map<LocationDto>(checkedLocation);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="address">the address of the location that we want to get</param> 
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="LocationDto"/></returns>
        public async Task<LocationDto> GetLocationByAddressAsync(string address, CancellationToken cancellationToken)
        {
            LocationDto locationDto = new LocationDto(); 
            var mappedLocation = _mapper.Map<Location>(locationDto);
            mappedLocation.Address = address;
            var checkedLocation = await _locationRepository.GetBySomethingAsync(x => x.Address == mappedLocation.Address, cancellationToken); 
            if (checkedLocation == null)
            {
                throw new NotFoundException("This location wasn't found");
            }
            return _mapper.Map<LocationDto>(checkedLocation);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="city">the city of the location that we want to get</param> 
        /// <param name="cancellationToken"></param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="LocationDto"/></returns>
        public async Task<LocationDto> GetLocationByCityAsync(string city, CancellationToken cancellationToken)
        {
            LocationDto locationDto = new LocationDto();
            var mappedLocation = _mapper.Map<Location>(locationDto);
            mappedLocation.City = city;
            var checkedLocation = await _locationRepository.GetBySomethingAsync(x => x.City == mappedLocation.City, cancellationToken);
            if (checkedLocation == null)
            {
                throw new NotFoundException("This location wasn't found");
            }
            return _mapper.Map<LocationDto>(checkedLocation);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="country">the country of the location that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="LocationDto"/></returns>
        public async Task<LocationDto> GetLocationByCountryAsync(string country, CancellationToken cancellationToken)
        {
            LocationDto locationDto = new LocationDto();
            var mappedLocation = _mapper.Map<Location>(locationDto);
            mappedLocation.Country = country; 
            var checkedLocation = await _locationRepository.GetBySomethingAsync(x => x.Country == mappedLocation.Country, cancellationToken);
            if (checkedLocation == null)
            {
                throw new NotFoundException("This location wasn't found");
            }
            return _mapper.Map<LocationDto>(checkedLocation);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="postalcode">the postalcode of the location that we want to get</param>
        /// <param name="cancellationToken">th cancellation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="LocationDto"/></returns>
        public async Task<LocationDto> GetLocationByPostalCodeAsync(string postalcode, CancellationToken cancellationToken)
        {
            LocationDto locationDto = new LocationDto();
            var mappedLocation = _mapper.Map<Location>(locationDto);
            mappedLocation.PostalCode = postalcode;
            var checkedLocation = await _locationRepository.GetBySomethingAsync(x => x.PostalCode == mappedLocation.PostalCode, cancellationToken);
            if (checkedLocation == null)
            {
                throw new NotFoundException("This location wasn't found");
            }
            return _mapper.Map<LocationDto>(checkedLocation);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="locationDto">the data transfer object </param>
        /// <param name="cancellationToken">the cancellation interne</param>
        /// /// <exception cref="NotFoundException">the not found exception</exception>
        /// <exception cref="ArgumentException">the argument exception</exception>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="LocationDto"/></returns> 
        public async Task<LocationDto> UpdateAsync(LocationDto locationDto, CancellationToken cancellationToken)
        {
            var mappedLocation = _mapper.Map<Location>(locationDto);
            var checkedLocation = await _locationRepository.GetBySomethingAsync(x => x.Id == mappedLocation.Id, cancellationToken); 
            if (checkedLocation == null)
            {
                throw new NotFoundException("This location wasn't found");
            }
            try
            {
                _locationRepository.UpdateAsync(mappedLocation);
                await _saveChangesRepository.SaveChangesAsync();
                _logger.LogInfo("Changes successfully saved in the database");
            }
            catch(Exception ex)
            {
                throw new ArgumentException($"Something went wrong while adding the location {ex.Message}");
            }
            return locationDto; 
        }
    }
}
