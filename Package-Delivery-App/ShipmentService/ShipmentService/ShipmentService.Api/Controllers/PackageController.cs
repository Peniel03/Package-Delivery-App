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
    /// the package controller
    /// </summary>
    [Route("api/persons/{personId}/packages")] 
    [ApiController]
    public class PackageController : ControllerBase
    {
        
        private readonly IPackageService _packageService;
        private readonly IMapper _mapper;

        /// <summary>
        /// initialization of a new instance of <see cref="PackageController"/>
        /// </summary>
        /// <param name="packageService">the package servcice</param>
        /// <param name="mapper">the mapper</param>
        public PackageController(IPackageService packageService, IMapper mapper)
        {
            _packageService = packageService;
            _mapper = mapper;
        }

        /// <summary>
        /// Function to create a new package.
        /// </summary>
        /// <param name="packagerequest">The package that we want to create</param>
        /// <param name="CancellationToken">The cancellation token from the http request</param>
        /// <returns>OK if the package has been created or badrequest if not</returns>
        [HttpPost("create/")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePackage([FromBody]  PackageRequest packagerequest, CancellationToken CancellationToken)
        {
            var result = await _packageService.AddAsync(_mapper.Map<PackageDto>(packagerequest), CancellationToken);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("Could not create the package");
        }

        /// <summary>
        /// Function to get all packages
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the package exists or badrequest if not</returns>
        [HttpGet("get-all")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPackages(CancellationToken cancellationToken)
        {
            var PackagesList = await _packageService.GetAllAsync(cancellationToken);

            if (PackagesList == null)
            {
                return BadRequest("There is no packages");
            }

            return Ok(PackagesList);
        }

        /// <summary>
        /// function to get the package by id
        /// </summary>
        /// <param name="id">the id of the package</param>
        /// <param name="packagerequest">the package that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the package exists or badrequest if not</returns>
        [HttpGet("get/{id}")] 
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPackageById(int id, [FromBody] PackageRequest packagerequest, CancellationToken cancellationToken)
        {
            var package = _mapper.Map<PackageDto>(packagerequest);
            package.Id = id;
            var result = await _packageService.GetByIdAsync(package, cancellationToken);

            if (result == null)
            {
                return BadRequest("This package does not exist");
            }

            return Ok(package);
        }


        /// <summary>
        /// Function to get the package by dimensions.
        /// </summary>
        /// <param name="dimensions">the dimensions of the package</param>
        /// <param name="packagerequest">the package that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the package exists or badrequest if not</returns>
        [HttpGet("get/{dimensions}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetlocationByDimensions(string dimensions, [FromBody] PackageRequest packagerequest, CancellationToken cancellationToken)
        {
            var package = _mapper.Map<PackageDto>(packagerequest);
            package.Dimensions = dimensions;
            var result = await _packageService.GetPackageByDimensionsAsync(package, cancellationToken);

            if (result == null)
            {
                return BadRequest("This package does not exist");
            }

            return Ok(package);
        }


        /// <summary>
        /// function to get the package by owner id
        /// </summary>
        /// <param name="ownerid">the id of the package's owner</param>
        /// <param name="packagerequest">the package that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the package exists or badrequest if not</returns>
        [HttpGet("get/{ownerid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetlocationByOwnerId(int ownerid, [FromBody] PackageRequest packagerequest, CancellationToken cancellationToken)
        {
            var package = _mapper.Map<PackageDto>(packagerequest);
            package.OwnerId = ownerid;
            var result = await _packageService.GetPackageByOwnerIdAsync(package, cancellationToken);

            if (result == null)
            {
                return BadRequest("This package does not exist");
            }

            return Ok(package);
        }


        /// <summary>
        /// function to get the package by weight
        /// </summary>
        /// <param name="weight">the weight of the package</param>
        /// <param name="packagerequest">the package that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the package exists or badrequest if not</returns>
        [HttpGet("get/{weight}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetlocationByWeight(decimal weight, [FromBody] PackageRequest packagerequest, CancellationToken cancellationToken)
        {
            var package = _mapper.Map<PackageDto>(packagerequest);
            package.Weight = weight;
            var result = await _packageService.GetPackageByWeightAsync(package, cancellationToken);

            if (result == null)
            {
                return BadRequest("This package does not exist");
            }

            return Ok(package);
        }


        /// <summary>
        /// Function to update the package
        /// </summary>
        /// <param name="id">The id of the package</param>
        /// <param name="locationRequest">The package that we have to update</param>
        /// <param name="CancellationTokenoken">the cancellation token</param>
        /// <returns>OK if the package has been updated and bad request if the package has not been updated</returns>
        [HttpPut("update/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateLocation(int id, [FromBody] PackageRequest packagerequest, CancellationToken CancellationToken)
        {
            var package = _mapper.Map<PackageDto>(packagerequest);
            package.Id = id;

            var result = await _packageService.UpdateAsync(package, CancellationToken);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("Could not update the package");
        }

        /// <summary>
        /// Functon to delete the package
        /// </summary>
        /// <param name="id">The id of the package</param>
        /// <param name="token">The token coming from the Http request</param>
        /// <returns>Ok if the package has been deleted and a bad request if the package has not been deleted</returns>
        [HttpDelete("delete/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteLocation(int id, [FromBody] PackageRequest packagerequest, CancellationToken CancellationToken) 
        {
            var package = _mapper.Map<PackageDto>(packagerequest);
            package.Id = id;

            var result = await _packageService.DeleteAsync(package, CancellationToken);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("Could not delete the package");
        }
    }
}
