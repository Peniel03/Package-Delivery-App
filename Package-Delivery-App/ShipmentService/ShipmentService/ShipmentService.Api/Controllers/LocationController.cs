using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShipmentService.Api.Request;
using ShipmentService.BusinessLogic.DTOs;
using ShipmentService.BusinessLogic.Interfaces;
using System.Net;
using System.Threading;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        private readonly IMapper _mapper;

        /// <summary>
        /// initialization of a new instance of <see cref="LocationController"/>
        /// </summary>
        /// <param name="locationService">the location service</param>
        /// <param name="mapper">the mapper</param>
        public LocationController(ILocationService locationService,IMapper mapper)
        {
            _locationService = locationService;
            _mapper = mapper;   
        }

        /// <summary>
        /// Function To create a new location
        /// </summary>
        /// <param name="locationrequest">The location that we want to create</param>
        /// <param name="CancellationToken">The cancellation token from the http request</param>
        /// <returns>OK if the location has been created or badrequest if not</returns>
        [HttpPost("create/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateLocation([FromBody] LocationRequest locationrequest, CancellationToken CancellationToken) 
        {
            var result = await _locationService.AddAsync(_mapper.Map<LocationDto>(locationrequest), CancellationToken);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("Could not create the location"); 
        }

        /// <summary>
        /// Function to get all locations
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the locations exist or Badrequest if not</returns>
        [HttpGet("get-all/")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllLocations(CancellationToken cancellationToken)
        {
            var LocationList = await _locationService.GetAllAsync(cancellationToken);

            if (LocationList == null)
            {
                return BadRequest("There is no locations");
            }

            return Ok(LocationList);
        }

        /// <summary>
        /// function to get the location by location id
        /// </summary>
        /// <param name="id">the id of the location</param>
        /// <param name="locationrequest">the location that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the location exists or Badrequest if not</returns>
        [HttpGet("get/{id}")] 
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetlocationById(int id, [FromBody] LocationRequest locationrequest,CancellationToken cancellationToken)
        {
             var location = _mapper.Map<LocationDto>(locationrequest);
             location.Id = id;
            var result = await _locationService.GetByIdAsync(location, cancellationToken);

            if (result == null)
            {
                return BadRequest("This location does not exist"); 
            }

            return Ok(location);
        }


        /// <summary>
        /// Function to get the location by address.
        /// </summary>
        /// <param name="address">address the location</param>
        /// <param name="locationrequest">the location that want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the location exist or Badrequest if not</returns>
        [HttpGet("get/{address}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetlocationByAddress(string address ,[FromBody] LocationRequest locationrequest, CancellationToken cancellationToken)
        {
            var location = _mapper.Map<LocationDto>(locationrequest);
            location.Address = address;
            var result = await _locationService.GetLocationByAddressAsync(location, cancellationToken);

            if (result == null)
            {
                return BadRequest("This location does not exist");
            }

            return Ok(location);
        }


        /// <summary>
        /// function to get the location by city
        /// </summary>
        /// <param name="city">the city of the location</param>
        /// <param name="locationrequest">the location that want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the location exist or Badrequest if not</returns>
        [HttpGet("get/{city}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetlocationByCity(string city, [FromBody] LocationRequest locationrequest, CancellationToken cancellationToken)
        {
            var location = _mapper.Map<LocationDto>(locationrequest);
            location.City = city;
            var result = await _locationService.GetLocationByCityAsync(location, cancellationToken);

            if (result == null)
            {
                return BadRequest("This location does not exist");
            }

            return Ok(location);
        }


        /// <summary>
        /// function to get the location by country
        /// </summary>
        /// <param name="country">the country of the location</param>
        /// <param name="locationrequest">the location that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the location exist or Badrequest if not</returns>
        [HttpGet("get/{country}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetlocationByCountry(string country, [FromBody] LocationRequest locationrequest, CancellationToken cancellationToken)
        {
            var location = _mapper.Map<LocationDto>(locationrequest);
            location.Country = country;
            var result = await _locationService.GetLocationByCountryAsync(location, cancellationToken);

            if (result == null)
            {
                return BadRequest("This location does not exist");
            }

            return Ok(location);
        }

        /// <summary>
        /// function to get the location by postal code
        /// </summary>
        /// <param name="postalcode">the postalcode of the location</param>
        /// <param name="locationrequest">the location that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the location exist and bad request if the location does not exist</returns>
        [HttpGet("get/{postalcode}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetlocationByPostalCode(string postalcode, [FromBody] LocationRequest locationrequest, CancellationToken cancellationToken)
        {
            var location = _mapper.Map<LocationDto>(locationrequest);
            location.PostalCode = postalcode;
            var result = await _locationService.GetLocationByPostalCodeAsync(location, cancellationToken);

            if (result == null)
            {
                return BadRequest("This location does not exist");
            }

            return Ok(location); 
        }

        /// <summary>
        /// Function to update the location
        /// </summary>
        /// <param name="id">The id of the location</param>
        /// <param name="locationRequest">The location that we have to update</param>
        /// <param name="CancellationTokenoken">the cancellation token</param>
        /// <returns>OK if the location has been updated and bad request if the location has not been updated</returns>
        [HttpPut("update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateLocation(int id, [FromBody] LocationRequest locationrequest, CancellationToken CancellationToken)
        {
            var location = _mapper.Map<LocationDto>(locationrequest);
            location.Id = id;

            var result = await _locationService.UpdateAsync(location, CancellationToken);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("Could not update the location");
        }

        /// <summary>
        /// Functon to delete the location
        /// </summary>
        /// <param name="id">The id of the location</param>
        /// <param name="token">The token coming from the Http request</param>
        /// <returns>Ok if the location has been deleted and a bad request if the location has not been deleted</returns>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteLocation(int id, [FromBody] LocationRequest locationRequest, CancellationToken CancellationToken)
        {
            var location = _mapper.Map<LocationDto>(locationRequest);
            location.Id = id;

            var result = await _locationService.DeleteAsync(location, CancellationToken);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("Could not delete the location");
        }

    }
}
