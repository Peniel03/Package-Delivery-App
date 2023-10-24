using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShipmentService.BusinessLogic.DTOs;
using ShipmentService.BusinessLogic.Interfaces;
using System.Net;

namespace ShipmentService.Api.Controllers
{
    /// <summary>
    /// the location controller
    /// </summary>
    [Route("api/locations")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        /// <summary>
        /// initialization of a new instance of <see cref="LocationController"/>
        /// </summary>
        /// <param name="locationService">the location service</param>
        /// <param name="mapper">the mapper</param>
        public LocationController(ILocationService locationService) 
        {
            _locationService = locationService;
        }

        /// <summary>
        /// Function To create a new location
        /// </summary>
        /// <param name="locationDto">The location that we want to create</param>
        /// <param name="CancellationToken">The cancellation token from the http request</param>
        /// <returns>OK if the location has been created or badrequest if not</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateLocation([FromBody] LocationDto locationDto, CancellationToken CancellationToken) 
        {
            var result = await _locationService.AddAsync(locationDto, CancellationToken);            
            return Ok(result);    
        }

        /// <summary>
        /// Function to get all locations
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the locations exist or Badrequest if not</returns>
        [HttpGet]  
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllLocations(CancellationToken cancellationToken)
        {
            var LocationList = await _locationService.GetAllAsync(cancellationToken);
            return Ok(LocationList);
        }

        /// <summary>
        /// function to get the location by location id
        /// </summary>
        /// <param name="id">the id of the location</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the location exists or Badrequest if not</returns>
        [HttpGet("{id}")]  
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetlocationById(int id, CancellationToken cancellationToken)
        {
            var result = await _locationService.GetByIdAsync(id, cancellationToken); 
            return Ok(result); 
        }

        /// <summary>
        /// Function to get the location by address.
        /// </summary>
        /// <param name="address">address the location</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the location exist or Badrequest if not</returns>
        [HttpGet("{address}")] 
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetlocationByAddress(string address , CancellationToken cancellationToken)
        {
             var result = await _locationService.GetLocationByAddressAsync(address, cancellationToken);
            return Ok(result); 
        }

        /// <summary>
        /// function to get the location by city
        /// </summary>
        /// <param name="city">the city of the location</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the location exist or Badrequest if not</returns>
        [HttpGet("{city}")] 
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetlocationByCity(string city, CancellationToken cancellationToken)
        {
            var result = await _locationService.GetLocationByCityAsync(city, cancellationToken);
            return Ok(result);
        }
         
        /// <summary>
        /// function to get the location by country
        /// </summary>
        /// <param name="country">the country of the location</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the location exist or Badrequest if not</returns>
        [HttpGet("{country}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetlocationByCountry(string country, CancellationToken cancellationToken)
        {
            var result = await _locationService.GetLocationByCountryAsync(country, cancellationToken);
            return Ok(result);
        }
           
        /// <summary>
        /// function to get the location by postal code
        /// </summary>
        /// <param name="postalcode">the postalcode of the location</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the location exist and bad request if the location does not exist</returns>
        [HttpGet("{postalcode}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetlocationByPostalCode(string postalcode, CancellationToken cancellationToken)
        {
            var result = await _locationService.GetLocationByPostalCodeAsync(postalcode, cancellationToken);
            return Ok(result); 
        }

        /// <summary>
        /// Function to update the location
        /// </summary>
        /// <param name="id">The id of the location</param>
        /// <param name="locationDto">The location that we have to update</param>
        /// <param name="CancellationToken">the cancellation token</param>
        /// <returns>OK if the location has been updated and bad request if the location has not been updated</returns>
        [HttpPut("{id}")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] 
        public async Task<IActionResult> UpdateLocation([FromBody] LocationDto locationDto, CancellationToken CancellationToken)
        {
            var result = await _locationService.UpdateAsync(locationDto, CancellationToken);
            return Ok(result); 
        }

        /// <summary>
        /// Functon to delete the location
        /// </summary>
        /// <param name="id">The id of the location</param>
        /// <param name="CancellationToken">The token coming from the Http request</param>
        /// <returns>Ok if the location has been deleted and a bad request if the location has not been deleted</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteLocation(int id, CancellationToken CancellationToken)
        { 
            var result = await _locationService.DeleteAsync(id, CancellationToken);
            return Ok(result);  
        } 
    }
}
