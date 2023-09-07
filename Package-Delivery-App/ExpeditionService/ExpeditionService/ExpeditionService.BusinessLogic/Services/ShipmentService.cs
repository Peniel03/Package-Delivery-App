using AutoMapper;
using ExpeditionService.BusinessLogic.DTOs;
using ExpeditionService.BusinessLogic.Exceptions;
using ExpeditionService.BusinessLogic.Helpers;
using ExpeditionService.BusinessLogic.Interfaces;
using ExpeditionService.DataAccess.Data;
using ExpeditionService.DataAccess.Interfaces;
using ExpeditionService.DataAccess.Models;

namespace ExpeditionService.BusinessLogic.Services
{
    /// <summary>
    /// the implementation of shipment service to manage shipments
    /// </summary>
    public class ShipmentService: IShipmentService
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
            var checkedShipement = await _shipmentRepository.GetBySomethingAsync(x => x.Id == mappedShipment.Id, cancellationToken);
            if (checkedShipement is not null)
            {
                _logger.LogError("Error occured while shipment the package");
                throw new AlreadyExistException("This shipment already exist");
            } 
            checkedShipement.EstimatedDeliveryDateTime = ShipmentHelperFunctions.SetEstimatedDeliveryDate(mappedShipment.EstimatedDeliveryDateTime);
            checkedShipement.ShipmentStatus = ShipmentStatus.Received;
            PackageDto packageDto = new PackageDto();
            checkedShipement.ShipmentCost = ShipmentHelperFunctions.SetShipmentCost(packageDto.Weight);
            checkedShipement.DeliveryMethod = ShipmentDeliveryMethod.SelfDelivery;
            _shipmentRepository.AddAsync(checkedShipement);
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
        /// <param name="id">the id of the shipment that we want to delete</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="ShipmentDto"/></returns>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <exception cref="ArgumentException">the argument exception</exception>
        public async Task<ShipmentDto> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            ShipmentDto shipmentDto = new ShipmentDto();
            var mappedShipment = _mapper.Map<Shipment>(shipmentDto);
            mappedShipment.Id = id;
            var checkedShipment = await _shipmentRepository.GetBySomethingAsync(x => x.Id == mappedShipment.Id, cancellationToken);
            if (checkedShipment == null)
            {
                throw new NotFoundException("The shipment wasn't found");
            }
            try
            {
                _shipmentRepository.DeleteAsync(mappedShipment);
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
            var list = await _shipmentRepository.GetAllAsync(cancellationToken);
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
        /// <param name="id">the id of the shipment we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="ShipmentDto"/></returns>
        public async Task<ShipmentDto> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            ShipmentDto shipmentDto = new ShipmentDto();
            var mappedShipement = _mapper.Map<Shipment>(shipmentDto);
            mappedShipement.Id = id; 
            var checkedShipment = await _shipmentRepository.GetBySomethingAsync(x => x.Id == mappedShipement.Id, cancellationToken);
            if (checkedShipment == null)
            {
                throw new NotFoundException("This shipment wasn't found");
            }
            return _mapper.Map<ShipmentDto>(checkedShipment);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="actualdeliverytime">the actual delivery date of the shipment we want to getparam>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>A <see cref="Task"/>that that contains <seealso cref="ShipmentDto"/></returns>
        public async Task<ShipmentDto> GetShipmentByActualDeliveryDateAsync(DateTimeOffset actualdeliverytime, CancellationToken cancellationToken)
        {
            ShipmentDto shipmentDto = new ShipmentDto();
            var mappedShipement = _mapper.Map<Shipment>(shipmentDto);
            mappedShipement.ActualDeliveryDateTime = actualdeliverytime;
            var checkedShipment = await _shipmentRepository.GetBySomethingAsync(x => x.ActualDeliveryDateTime == mappedShipement.ActualDeliveryDateTime, cancellationToken);
            if (checkedShipment == null)
            {
                throw new NotFoundException("This shipment wasn't found");
            }
            return _mapper.Map<ShipmentDto>(checkedShipment);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="cost">the cost of the shipment we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="ShipmentDto"/></returns>
        public async Task<ShipmentDto> GetShipmentByCostAsync(decimal cost, CancellationToken cancellationToken)
        {
            ShipmentDto shipmentDto = new ShipmentDto();
            var mappedShipement = _mapper.Map<Shipment>(shipmentDto);
            mappedShipement.ShipmentCost = cost; 
            var checkedShipment = await _shipmentRepository.GetBySomethingAsync(x => x.ShipmentCost == mappedShipement.ShipmentCost, cancellationToken);
            if (checkedShipment == null)
            {
                throw new NotFoundException("This shipment wasn't found");
            }
            return _mapper.Map<ShipmentDto>(checkedShipment);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="pickupdatetime">the pickup datetime of the shipment we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="ShipmentDto"/></returns>
        public async Task<ShipmentDto> GetShipmentByPickUpDateTimeAsync(DateTimeOffset pickupdatetime, CancellationToken cancellationToken)
        {
            ShipmentDto shipmentDto = new ShipmentDto();
            var mappedShipement = _mapper.Map<Shipment>(shipmentDto);
            mappedShipement.PickupDateTime = pickupdatetime;
            var checkedShipment = await _shipmentRepository.GetBySomethingAsync(x => x.PickupDateTime == mappedShipement.PickupDateTime, cancellationToken);
            if (checkedShipment == null)
            {
                throw new NotFoundException("This shipment wasn't found");
            }
            return _mapper.Map<ShipmentDto>(checkedShipment);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="status">the status of the shipment that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException">the not found excetion</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="ShipmentDto"/></returns>
        public async Task<ShipmentDto> GetShipmentByStatusAsync(string status, CancellationToken cancellationToken)
        {
            ShipmentDto shipmentDto = new ShipmentDto();
            var mappedShipement = _mapper.Map<Shipment>(shipmentDto);
            mappedShipement.ShipmentStatus = (ShipmentStatus)Enum.Parse(typeof(ShipmentStatus), status);
            var checkedShipment = await _shipmentRepository.GetBySomethingAsync(x => x.ShipmentStatus.ToString() == mappedShipement.ShipmentStatus.ToString(), cancellationToken);
            if (checkedShipment == null)
            {
                throw new NotFoundException("This shipment wasn't found"); 
            }
            return _mapper.Map<ShipmentDto>(checkedShipment);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="trackingnumber">the tracking number of the shipment that we want to get </param>
        /// <param name="cancellationToken">the cancellatiuon token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="ShipmentDto"/></returns> 
        public async Task<ShipmentDto> GetShipmentByTrackingNumberAsync(string trackingnumber, CancellationToken cancellationToken)
        {
            ShipmentDto shipmentDto = new ShipmentDto();
            var mappedShipement = _mapper.Map<Shipment>(shipmentDto);
            mappedShipement.TrackingNumber = trackingnumber;
            var checkedShipment = await _shipmentRepository.GetBySomethingAsync(x => x.TrackingNumber == mappedShipement.TrackingNumber, cancellationToken);
            if (checkedShipment == null)
            {
                throw new NotFoundException("This shipment wasn't found");
            }
            return _mapper.Map<ShipmentDto>(checkedShipment);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="shipmentDto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public async Task<ShipmentDto> UpdateAsync(ShipmentDto shipmentDto, CancellationToken cancellationToken)
        {
            var mappedShipement = _mapper.Map<Shipment>(shipmentDto);
            var checkedShipment = await _shipmentRepository.GetBySomethingAsync(x => x.Id == mappedShipement.Id, cancellationToken);
            if (checkedShipment == null)
            {
                _logger.LogError("the edition was not updated because it does not exist");
                throw new NotFoundException("This shipment wasn't found");
            }
            try
            {
                _shipmentRepository.UpdateAsync(mappedShipement);
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
