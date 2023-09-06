using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShipmentService.Api.Request;
using ShipmentService.BusinessLogic.DTOs;
using ShipmentService.BusinessLogic.Interfaces;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShipmentService.Api.Controllers
{
    /// <summary>
    /// the shipemnt controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {

        private readonly IShipmentService _shipmentService;
        private readonly IMapper _mapper;

        /// <summary>
        /// initialization of a new instance of <see cref="ShipmentController"/>
        /// </summary>
        /// <param name="shipmentService">the shipement servcice</param>
        /// <param name="mapper">the mapper</param>
        public ShipmentController(IShipmentService shipmentService, IMapper mapper)  
        {
            _shipmentService = shipmentService;
            _mapper = mapper;
        }

        /// <summary>
        /// Function to create a new shipment.
        /// </summary>
        /// <param name="shipmentrequest">The shiment that we want to create</param>
        /// <param name="CancellationToken">The cancellation token from the http request</param>
        /// <returns>OK if the shipment has been created or badrequest if not</returns>
        [HttpPost("create-shipment/{shipmentrequest,CancellationToken}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateShipment([FromBody] ShipmentCreateRequest shipmentrequest, CancellationToken CancellationToken) 
        {
            var result = await _shipmentService.AddAsync(_mapper.Map<ShipmentDto>(shipmentrequest), CancellationToken);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("Could not create the shipment");
        }

        /// <summary>
        /// Function to get all shipments
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the shipment exists or badrequest if not</returns>
        [HttpGet("get-all-shipments/{cancellationToken}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllShipments(CancellationToken cancellationToken)
        {
            var ShipmentsList = await _shipmentService.GetAllAsync(cancellationToken);

            if (ShipmentsList == null)
            {
                return BadRequest("There is no shipments");
            }

            return Ok(ShipmentsList);
        }

        /// <summary>
        /// function to get the shipement by id
        /// </summary>
        /// <param name="id">the id of the shipment</param>
        /// <param name="shipmentrequest">the shipment that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the shipment exists or badrequest if not</returns>
        [HttpGet("get-shipemnt-by-id/{id,shipmentrequest,cancellationToken}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetShipmentById(int id, [FromBody] ShipmentCreateRequest shipmentrequest, CancellationToken cancellationToken) 
        {
            var shipment = _mapper.Map<ShipmentDto>(shipmentrequest); 
            shipment.Id = id;
            var result = await _shipmentService.GetByIdAsync(shipment, cancellationToken);

            if (result == null)
            {
                return BadRequest("This shipment does not exist"); 
            }

            return Ok(shipment);
        }


        /// <summary>
        /// Function to get the shipment by tracking number.
        /// </summary>
        /// <param name="trackingnumber">the tracking number of the shipment</param>
        /// <param name="shipmentrequest">the shipment that we want to get</param> 
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the shipment exists or badrequest if not</returns> 
        [HttpGet("get-shipemnt-by-trackingnumber/{trackingnumber,shipmentrequest,cancellationToken}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetShipmentByTrackingNumber(string trackingnumber, [FromBody] ShipmentCreateRequest shipmentrequest, CancellationToken cancellationToken)
        {
            var shipment = _mapper.Map<ShipmentDto>(shipmentrequest);
            shipment.TrackingNumber = trackingnumber;
            var result = await _shipmentService.GetShipmentByTrackingNumberAsync(shipment, cancellationToken);

            if (result == null)
            {
                return BadRequest("This shipment does not exist");
            }

            return Ok(shipment); 
        }


        /// <summary>
        /// function to get the shipment by pick up date
        /// </summary>
        /// <param name="pickupdatetime">the pickup datetime of the shipment</param>
        /// <param name="shipmentrequest">the shipment that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the shipment exists or badrequest if not</returns>
        [HttpGet("get-shipment-by-pickupdatetime/{pickupdatetime,shipmentrequest,cancellationToken}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetShipmentByPickUpDateTime(DateTimeOffset pickupdatetime, [FromBody] ShipmentCreateRequest shipmentrequest, CancellationToken cancellationToken)
        {
            var shipment = _mapper.Map<ShipmentDto>(shipmentrequest);
            shipment.PickupDateTime = pickupdatetime; 
            var result = await _shipmentService.GetShipmentByPickUpDateTimeAsync(shipment, cancellationToken);
             
            if (result == null)
            {
                return BadRequest("This shipment does not exist");
            }

            return Ok(shipment); 
        }


        /// <summary>
        /// function to get the shipment by actual delivery  date
        /// </summary>
        /// <param name="actualdeliverydate">the actual delivery datetime of the shipment</param>
        /// <param name="shipmentrequest">the shipment that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the shipment exists or badrequest if not</returns>
        [HttpGet("get-shipment-by-pickupdatetime/{actualdeliverydate,shipmentrequest,cancellationToken}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetShipmentByActualDeliveryDateTime(DateTimeOffset actualdeliverydate, [FromBody] ShipmentCreateRequest shipmentrequest, CancellationToken cancellationToken)
        {
            var shipment = _mapper.Map<ShipmentDto>(shipmentrequest);
            shipment.ActualDeliveryDateTime = actualdeliverydate;
            var result = await _shipmentService.GetShipmentByPickUpDateTimeAsync(shipment, cancellationToken);
             
            if (result == null)
            {
                return BadRequest("This shipment does not exist");
            }

            return Ok(shipment);
        }


        /// function to get the shipment by cost
        /// </summary>
        /// <param name="cost">the cost of the shipment</param>
        /// <param name="shipmentrequest">the shipment that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the shipment exists or badrequest if not</returns>
        [HttpGet("get-shipment-by-cost/{cost,shipmentrequest,cancellationToken}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetShipmentByCost(decimal cost, [FromBody] ShipmentCreateRequest shipmentrequest, CancellationToken cancellationToken)
        { 
            var shipment = _mapper.Map<ShipmentDto>(shipmentrequest);
            shipment.ShipmentCost = cost; 
            var result = await _shipmentService.GetShipmentByCostAsync(shipment, cancellationToken);

            if (result == null)
            {
                return BadRequest("This shipment does not exist");
            }

            return Ok(shipment);
        }

        /// function to get the shipment by status
        /// </summary>
        /// <param name="status">the status of the shipment</param>  
        /// <param name="shipmentrequest">the shipment that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the shipment exists or badrequest if not</returns>
        [HttpGet("get-shipment-by-status/{status,shipmentrequest,cancellationToken}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetShipmentByStatus(string status, [FromBody] ShipmentCreateRequest shipmentrequest, CancellationToken cancellationToken)
        {
            var shipment = _mapper.Map<ShipmentDto>(shipmentrequest);
            shipment.ShipmentStatus = status;
            var result = await _shipmentService.GetShipmentByStatusAsync(shipment, cancellationToken);

            if (result == null)
            {
                return BadRequest("This shipment does not exist");
            }

            return Ok(shipment);
        }


        /// <summary>
        /// Function to update the shipment
        /// </summary>
        /// <param name="id">The id of the shipment</param>
        /// <param name="shipmentrequest">The shipment that we have to update</param>
        /// <param name="CancellationToken">the cancellation token</param>
        /// <returns>OK if the shipment has been updated and bad request if the shipment has not been updated</returns>
        [HttpPut("update-shipment/{id,shipmentrequest,cancellationToken}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateShipment(int id, [FromBody] ShipmentUpdateRequest shipmentrequest, CancellationToken CancellationToken)
        {
            var shipment = _mapper.Map<ShipmentDto>(shipmentrequest);
            shipment.Id = id;

            var result = await _shipmentService.UpdateAsync(shipment, CancellationToken);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("Could not update the shipment"); 
        }

        /// <summary>
        /// Functon to delete the shipment
        /// </summary>
        /// <param name="id">The id of the shipment</param>
        /// <param name="CancellationToken">The token coming from the Http request</param>
        /// <returns>Ok if the shipment has been deleted and a bad request if the shipment has not been deleted</returns>
        [HttpDelete("delete-shipment/{id,shipmentrequest,cancellationToken}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteShipment(int id, [FromBody] ShipmentCreateRequest shipmentrequest, CancellationToken CancellationToken)
        {
            var shipment = _mapper.Map<ShipmentDto>(shipmentrequest);
            shipment.Id = id;

            var result = await _shipmentService.DeleteAsync(shipment, CancellationToken);

            if (result != null)
            {
                return Ok(result); 
            }

            return BadRequest("Could not delete the shipment"); 
        }

    }
}
