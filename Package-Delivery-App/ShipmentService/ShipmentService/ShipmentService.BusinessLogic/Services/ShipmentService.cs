using AutoMapper;
using ShipmentService.BusinessLogic.DTOs;
using ShipmentService.BusinessLogic.Exceptions;
using ShipmentService.BusinessLogic.Helpers;
using ShipmentService.BusinessLogic.Interfaces;
using ShipmentService.DataAccess.Interfaces;
using ShipmentService.DataAccess.Models;


namespace ShipmentService.BusinessLogic.Services
{
    /// <summary>
    /// the implementation of shipment service to manage shipments
    /// </summary>
    public class ShipmentService : IShipmentService
    {
        private readonly IShipmentRepository _shipmentRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
        private readonly ISaveChangesRepository _saveChangesRepository;

        /// <summary>
        /// initialization of a new instance of <see cref="ShipmentService"/>
        /// </summary>
        /// <param name="shipmentRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="saveChangesRepository"></param>
        public ShipmentService(IShipmentRepository shipmentRepository,
            IMapper mapper,
           ILoggerManager logger,
            ISaveChangesRepository saveChangesRepository)
        {
            _shipmentRepository = shipmentRepository;    
            _mapper = mapper;
            _logger = logger;
            _saveChangesRepository = saveChangesRepository;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="shipmentDto">the shipment's data transfert object</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="ShipmentDto"/></returns>
        /// <exception cref="AlreadyExistException">the already exist exception</exception>
        /// <exception cref="ArgumentException">the argument exception</exception>
        public async Task<ShipmentDto> AddAsync(ShipmentDto shipmentDto, CancellationToken cancellationToken)
        {
            var mappedShipment = _mapper.Map<Shipment>(shipmentDto);
            var checkedShipement = await _shipmentRepository.GetById(mappedShipment.Id, cancellationToken);

            if (checkedShipement != null)
            {
                _logger.LogError("Error occured while shipment the package");
                throw new AlreadyExistException("This shipment already exist");
            }
            checkedShipement.TrackingNumber = ShipmentHelperFunctions.TrackingNumberGenerator();
            checkedShipement.PickupDateTime = DateTimeOffset.UtcNow;
            _shipmentRepository.Add(checkedShipement);

            try
            {
                await _saveChangesRepository.SaveChangesAsync();
                _logger.LogInfo("Changes successfully saved in the database");
            }
            catch (Exception ex)
            {
                _logger.LogInfo($"Error occured while adding a shipment{ex.Message}");
                throw new ArgumentException($"Something went wrong while adding the shipment {ex.Message}");
            }
            return shipmentDto; 
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="shipmentDto">the shipment data transfer object</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="ShipmentDto"/></returns>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <exception cref="ArgumentException">the argument exception</exception>
        public async Task<ShipmentDto> DeleteAsync(ShipmentDto shipmentDto, CancellationToken cancellationToken)
        {
            var mappedShipment = _mapper.Map<Shipment>(shipmentDto);
            var checkedShipment = await _shipmentRepository.GetById(mappedShipment.Id, cancellationToken);
            if (checkedShipment == null)
            {
                throw new NotFoundException("The shipment wasn't found");
            }

            try
            {
                _shipmentRepository.Delete(mappedShipment);
                await _saveChangesRepository.SaveChangesAsync();
                _logger.LogInfo("Changes successfully saved in the database");
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Something went wrong while deleting the shipment {ex.Message}", ex);
            }
            return shipmentDto;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/>that contains a list of <seealso cref="ShipmentDto"/></returns> 
        public async Task<List<ShipmentDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var list = await _shipmentRepository.GetAll(cancellationToken); 
            if (list == null)
            {
                throw new NotFoundException("There no registered shipments yet");
            }

            var listDto = _mapper.Map<List<ShipmentDto>>(list);

            return listDto;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="shipmentDto">the shipment data transfer object</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="ShipmentDto"/></returns>
        public async Task<ShipmentDto> GetByIdAsync(ShipmentDto shipmentDto, CancellationToken cancellationToken)
        {
            var mappedShipement = _mapper.Map<Shipment>(shipmentDto);
            var checkedShipment = await _shipmentRepository.GetById(mappedShipement.Id, cancellationToken); 
            if (checkedShipment == null)
            {
                throw new NotFoundException("This shipment wasn't found");
            }
            return _mapper.Map<ShipmentDto>(checkedShipment); 
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="shipmentDto">the shipment data transfer object</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>A <see cref="Task"/>that that contains <seealso cref="ShipmentDto"/></returns>
        public async Task<ShipmentDto> GetShipmentByActualDeliveryDateAsync(ShipmentDto shipmentDto, CancellationToken cancellationToken)
        {
            var mappedShipement = _mapper.Map<Shipment>(shipmentDto);
            var checkedShipment = await _shipmentRepository.GetShipmentByActualDeliveryDate(mappedShipement.ActualDeliveryDateTime, cancellationToken);
            if (checkedShipment == null)
            {
                throw new NotFoundException("This shipment wasn't found");
            }
            return _mapper.Map<ShipmentDto>(checkedShipment); 
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="shipmentDto">the shipment datat transfer object</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="ShipmentDto"/></returns>
        public async Task<ShipmentDto> GetShipmentByCostAsync(ShipmentDto shipmentDto, CancellationToken cancellationToken)
        {
            var mappedShipement = _mapper.Map<Shipment>(shipmentDto);
            var checkedShipment = await _shipmentRepository.GetShipmentByCost(mappedShipement.ShipmentCost, cancellationToken);
            if (checkedShipment == null)
            {
                throw new NotFoundException("This shipment wasn't found");
            }
            return _mapper.Map<ShipmentDto>(checkedShipment); 
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="shipmentDto">the shipment data transfer object</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="ShipmentDto"/></returns>
        public async Task<ShipmentDto> GetShipmentByPickUpDateTimeAsync(ShipmentDto shipmentDto, CancellationToken cancellationToken)
        {
            var mappedShipement = _mapper.Map<Shipment>(shipmentDto);
            var checkedShipment = await _shipmentRepository.GetShipmentByPickUpDateTime(mappedShipement.PickupDateTime, cancellationToken);
            if (checkedShipment == null)
            {
                throw new NotFoundException("This shipment wasn't found");
            }
            return _mapper.Map<ShipmentDto>(checkedShipment); 
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="shipmentDto">the shipment data transfer object</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException">the not found excetion</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="ShipmentDto"/></returns>
        public async Task<ShipmentDto> GetShipmentByStatusAsync(ShipmentDto shipmentDto, CancellationToken cancellationToken)
        {
            var mappedShipement = _mapper.Map<Shipment>(shipmentDto);
            var checkedShipment = await _shipmentRepository.GetShipmentByStatus(mappedShipement.ShipmentStatus, cancellationToken);
            if (checkedShipment == null)
            {
                throw new NotFoundException("This shipment wasn't found");
            }
            return _mapper.Map<ShipmentDto>(checkedShipment);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="shipmentDto">the shipment data transfert object </param>
        /// <param name="cancellationToken">the cancellatiuon token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="ShipmentDto"/></returns> 
        public async Task<ShipmentDto> GetShipmentByTrackingNumberAsync(ShipmentDto shipmentDto, CancellationToken cancellationToken)
        {
            var mappedShipement = _mapper.Map<Shipment>(shipmentDto);
            var checkedShipment = await _shipmentRepository.GetShipmentByTrackingNumber(mappedShipement.TrackingNumber, cancellationToken);
            if (checkedShipment == null)
            {
                throw new NotFoundException("This shipment wasn't found");
            }
            return _mapper.Map<ShipmentDto>(checkedShipment);
        }

        public async Task<ShipmentDto> UpdateAsync(ShipmentDto shipmentDto, CancellationToken cancellationToken)
        {
            var mappedShipement = _mapper.Map<Shipment>(shipmentDto);
            var checkedShipment = await _shipmentRepository.GetById(mappedShipement.Id, cancellationToken);
            if (checkedShipment == null)
            {
                _logger.LogError("the edition was not updated because it does not exist");
                throw new NotFoundException("This shipment wasn't found");
            }
            try
            {
                _shipmentRepository.Update(mappedShipement);
                await _saveChangesRepository.SaveChangesAsync();
                _logger.LogInfo("Changes successfully saved in the database");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while updating the shipment");
                throw new ArgumentException($"Something went wrong while updating the shipment to the database {ex.Message}");
            }

            return shipmentDto; 
        }

    }
}
