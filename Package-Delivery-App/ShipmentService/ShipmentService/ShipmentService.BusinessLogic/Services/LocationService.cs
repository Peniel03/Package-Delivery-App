using AutoMapper;
using Microsoft.Extensions.Logging;
using ShipmentService.BusinessLogic.DTOs;
using ShipmentService.BusinessLogic.Exceptions;
using ShipmentService.BusinessLogic.Interfaces;
using ShipmentService.DataAccess.Interfaces;
using ShipmentService.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.BusinessLogic.Services
{
    /// <summary>
    /// the implementation of location service to manage locations
    /// </summary>
    public class LocationService : ILocationService 
    {
        private readonly ILocationRepository _locationRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<LocationService> _logger;
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
            ILogger<LocationService> logger, 
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
            var checkedLocation = await _locationRepository.GetById(mappedLocation.Id, cancellationToken);

            //if (checkedLocation != null)
            //{
            //    _logger.LogError("Error occured while adding the location");
            //    throw new AlreadyExistException("This location already exist");
            //}

            _locationRepository.Add(mappedLocation);

            try
            {
                await _saveChangesRepository.SaveChangesAsync();
                _logger.LogInformation("Changes successfully saved in the database");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error occured while adding a location{ex.Message}",ex);  
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

        public async Task<LocationDto> DeleteAsync(LocationDto locationDto, CancellationToken cancellationToken)
        {
            var mappedLocation = _mapper.Map<Location>(locationDto);
            var checkedBook = await _locationRepository.GetById(mappedLocation.Id, cancellationToken);
            if(checkedBook == null)
            {
                throw new NotFoundException("The location wasn't found");
            }

            try
            {
                _locationRepository.Delete(mappedLocation);
                await _saveChangesRepository.SaveChangesAsync();
                _logger.LogInformation("Changes successfully saved in the database");
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
            var list = await _locationRepository.GetAll(cancellationToken);
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
        /// <param name="locationDto">the location data transfer object</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="LocationDto"/></returns>
        public async Task<LocationDto> GetByIdAsync(LocationDto locationDto, CancellationToken cancellationToken)
        {
            var mappedLocation = _mapper.Map<Location>(locationDto);
            var checkedLocation = await _locationRepository.GetById(mappedLocation.Id, cancellationToken);
            if(checkedLocation == null)
            {
                throw new NotFoundException("This location wasn't found");
            }
            return _mapper.Map<LocationDto>(checkedLocation);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="locationDto">the location data transfer object</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="LocationDto"/></returns>
        public async Task<LocationDto> GetLocationByAddressAsync(LocationDto locationDto, CancellationToken cancellationToken)
        {
            var mappedLocation = _mapper.Map<Location>(locationDto);
            var checkedLocation = await _locationRepository.GetLocationByAddress(mappedLocation.Address, cancellationToken);
            if (checkedLocation == null)
            {
                throw new NotFoundException("This location wasn't found");
            }
            return _mapper.Map<LocationDto>(checkedLocation);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="locationDto"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="LocationDto"/></returns>
        public async Task<LocationDto> GetLocationByCityAsync(LocationDto locationDto, CancellationToken cancellationToken)
        {
            var mappedLocation = _mapper.Map<Location>(locationDto);
            var checkedLocation = await _locationRepository.GetLocationByCity(mappedLocation.City, cancellationToken);
            if (checkedLocation == null)
            {
                throw new NotFoundException("This location wasn't found");
            }
            return _mapper.Map<LocationDto>(checkedLocation);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="locationDto">the location's data transfer object</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="LocationDto"/></returns>
        public async Task<LocationDto> GetLocationByCountryAsync(LocationDto locationDto, CancellationToken cancellationToken)
        {
            var mappedLocation = _mapper.Map<Location>(locationDto);
            var checkedLocation = await _locationRepository.GetLocationByCountry(mappedLocation.Country, cancellationToken);
            if (checkedLocation == null)
            {
                throw new NotFoundException("This location wasn't found");
            }
            return _mapper.Map<LocationDto>(checkedLocation);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="locationDto">the location's data transfert object</param>
        /// <param name="cancellationToken">th cancellation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="LocationDto"/></returns>
        public async Task<LocationDto> GetLocationByPostalCodeAsync(LocationDto locationDto, CancellationToken cancellationToken)
        {
            var mappedLocation = _mapper.Map<Location>(locationDto);
            var checkedLocation = await _locationRepository.GetLocationByPostalCode(mappedLocation.PostalCode, cancellationToken);
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
            var checkedLocation = await _locationRepository.GetById(mappedLocation.Id, cancellationToken);
            if (checkedLocation == null)
            {
                throw new NotFoundException("This location wasn't found");

            }
            try
            {
                _locationRepository.Update(mappedLocation);
                await _saveChangesRepository.SaveChangesAsync();
                _logger.LogInformation("Changes successfully saved in the database");
            }
            catch(Exception ex)
            {
                throw new ArgumentException($"Something went wrong while adding the location {ex.Message}");
            }

            return locationDto;
        }
    }
}
