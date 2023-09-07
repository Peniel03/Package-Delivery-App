using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShipmentService.BusinessLogic.DTOs;
using ShipmentService.BusinessLogic.Interfaces;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShipmentService.Api.Controllers
{
    /// <summary>
    /// the shipemnt controller 
    /// </summary>
    [Route("api/packages/{packageId}/shipments")]
    [ApiController]
    public class ShipmentController : ControllerBase
    {
        private readonly IShipmentService _shipmentService;

        /// <summary>
        /// initialization of a new instance of <see cref="ShipmentController"/>
        /// </summary>
        /// <param name="shipmentService">the shipement servcice</param>
        /// <param name="mapper">the mapper</param>
        public ShipmentController(IShipmentService shipmentService)  
        {
            _shipmentService = shipmentService;
        }

        /// <summary>
        /// Function to create a new shipment.
        /// </summary>
        /// <param name="shipmentDto">The shiment that we want to create</param>
        /// <param name="CancellationToken">The cancellation token from the http request</param>
        /// <returns>OK if the shipment has been created or badrequest if not</returns>
        [HttpPost] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateShipment([FromBody] ShipmentDto shipmentDto, CancellationToken CancellationToken) 
        {
            var result = await _shipmentService.AddAsync(shipmentDto, CancellationToken); 
            return Ok(result); 
        }

        /// <summary>
        /// Function to get all shipments
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the shipment exists or badrequest if not</returns>
        [HttpGet] 
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllShipments(CancellationToken cancellationToken)
        {
            var ShipmentsList = await _shipmentService.GetAllAsync(cancellationToken); 
            return Ok(ShipmentsList);
        }

        /// <summary>
        /// function to get the shipement by id
        /// </summary>
        /// <param name="id">the id of the shipment</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the shipment exists or badrequest if not</returns>
        [HttpGet("{id}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetShipmentById(int id, CancellationToken cancellationToken) 
        {
            var result = await _shipmentService.GetByIdAsync(id, cancellationToken);
            return Ok(result); 
        }

        /// <summary>
        /// Function to get the shipment by tracking number.
        /// </summary>
        /// <param name="trackingnumber">the tracking number of the shipment</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the shipment exists or badrequest if not</returns> 
        [HttpGet("{trackingnumber}")] 
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetShipmentByTrackingNumber(string trackingnumber, CancellationToken cancellationToken)
        {
            var result = await _shipmentService.GetShipmentByTrackingNumberAsync(trackingnumber, cancellationToken); 
            return Ok(result); 
        }
         
        /// <summary>
        /// function to get the shipment by pick up date
        /// </summary>
        /// <param name="pickupdatetime">the pickup datetime of the shipment</param>
        /// <param name="shipmentrequest">the shipment that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the shipment exists or badrequest if not</returns>
        [HttpGet("{pickupdatetime}")] 
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetShipmentByPickUpDateTime(DateTimeOffset pickupdatetime, CancellationToken cancellationToken)
        {
             var result = await _shipmentService.GetShipmentByPickUpDateTimeAsync(pickupdatetime, cancellationToken);
            return Ok(result);  
        }
         
        /// <summary>
        /// function to get the shipment by actual delivery  date
        /// </summary>
        /// <param name="actualdeliverydate">the actual delivery datetime of the shipment</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the shipment exists or badrequest if not</returns>
        [HttpGet("{actualdeliverydate}")] 
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetShipmentByActualDeliveryDateTime(DateTimeOffset actualdeliverydate, CancellationToken cancellationToken)
        {
            var result = await _shipmentService.GetShipmentByPickUpDateTimeAsync(actualdeliverydate, cancellationToken); 
            return Ok(result);
        }

        /// function to get the shipment by cost
        /// </summary>
        /// <param name="cost">the cost of the shipment</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the shipment exists or badrequest if not</returns>
        [HttpGet("{cost}")]  
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetShipmentByCost(decimal cost, CancellationToken cancellationToken)
        { 
            var result = await _shipmentService.GetShipmentByCostAsync(cost, cancellationToken);
            return Ok(result);
        }

        /// function to get the shipment by status
        /// </summary>
        /// <param name="status">the status of the shipment</param>  
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the shipment exists or badrequest if not</returns>
        [HttpGet("{status}")]  
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetShipmentByStatus(string status, CancellationToken cancellationToken)
        {
            var result = await _shipmentService.GetShipmentByStatusAsync(status, cancellationToken);
            return Ok(result); 
        }

        /// <summary>
        /// Function to update the shipment
        /// </summary>
        /// <param name="id">The id of the shipment</param>
        /// <param name="shipmentDto">The shiment that we want to create</param>
        /// <param name="CancellationToken">the cancellation token</param>
        /// <returns>OK if the shipment has been updated and bad request if the shipment has not been updated</returns>
        [HttpPut("{id}")]  
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateShipment([FromBody] ShipmentDto shipmenDto, CancellationToken CancellationToken)
        {
             var result = await _shipmentService.UpdateAsync(shipmenDto, CancellationToken); 
            return Ok(result); 
        }

        /// <summary>
        /// Functon to delete the shipment
        /// </summary>
        /// <param name="id">The id of the shipment</param>
        /// <param name="CancellationToken">The token coming from the Http request</param>
        /// <returns>Ok if the shipment has been deleted and a bad request if the shipment has not been deleted</returns>
        [HttpDelete("{id}")]  
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteShipment(int id, CancellationToken CancellationToken)
        {
             var result = await _shipmentService.DeleteAsync(id, CancellationToken);
             return Ok(result); 
        }
    }
}
