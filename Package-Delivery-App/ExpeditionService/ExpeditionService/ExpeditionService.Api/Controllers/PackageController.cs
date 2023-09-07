using AutoMapper;
using ExpeditionService.BusinessLogic.DTOs;
using ExpeditionService.BusinessLogic.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ExpeditionService.Api.Controllers
{
    /// <summary>
    /// the package controller
    /// </summary>
    [Route("api/persons/{personId}/packages")] 
    [ApiController]
    public class PackageController : ControllerBase
    {
        private readonly IPackageService _packageService;
        /// <summary>
        /// initialization of a new instance of <see cref="PackageController"/>
        /// </summary>
        /// <param name="packageService">the package servcice</param>
        /// <param name="mapper">the mapper</param>
        public PackageController(IPackageService packageService, IMapper mapper)
        {
            _packageService = packageService;
        }

        /// <summary>
        /// Function to create a new package.
        /// </summary>
        /// <param name="packageDto">The package that we want to create</param> 
        /// <param name="CancellationToken">The cancellation token from the http request</param>
        /// <returns>OK if the package has been created or badrequest if not</returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePackage([FromBody] PackageDto packageDto, CancellationToken CancellationToken) 
        {
            var result = await _packageService.AddAsync(packageDto, CancellationToken); 
            return Ok(result);
        }

        /// <summary>
        /// Function to get all packages
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the package exists or badrequest if not</returns>
        [HttpGet] 
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPackages(CancellationToken cancellationToken)
        {
            var PackagesList = await _packageService.GetAllAsync(cancellationToken);
            return Ok(PackagesList);
        }

        /// <summary>
        /// function to get the package by id
        /// </summary>
        /// <param name="id">the id of the package</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the package exists or badrequest if not</returns>
        [HttpGet("{id}")] 
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPackageById(string id, CancellationToken cancellationToken)
        {
            var result = await _packageService.GetByIdAsync(id, cancellationToken);
            return Ok(result); 
        }

        /// <summary>
        /// Function to get the package by dimensions.
        /// </summary>
        /// <param name="dimensions">the dimensions of the package</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the package exists or badrequest if not</returns>
        [HttpGet("{dimensions}")] 
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetlocationByDimensions(string dimensions, CancellationToken cancellationToken)
        {
            var result = await _packageService.GetPackageByDimensionsAsync(dimensions, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// function to get the package by owner id
        /// </summary>
        /// <param name="ownerid">the id of the package's owner</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the package exists or badrequest if not</returns>
        [HttpGet("{ownerid}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetlocationByOwnerId(int ownerid, CancellationToken cancellationToken)
        {
            var result = await _packageService.GetPackageByOwnerIdAsync(ownerid, cancellationToken);
            return Ok(result);
        }

        /// <summary>
        /// function to get the package by weight
        /// </summary>
        /// <param name="weight">the weight of the package</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the package exists or badrequest if not</returns>
        [HttpGet("{weight}")] 
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetlocationByWeight(decimal weight, CancellationToken cancellationToken)
        {
            var result = await _packageService.GetPackageByWeightAsync(weight, cancellationToken);
            return Ok(result); 
        }

        /// <summary>
        /// Function to update the package
        /// </summary>
        /// <param name="locationRequest">The package that we have to update</param>
        /// <param name="CancellationTokenoken">the cancellation token</param>
        /// <returns>OK if the package has been updated and bad request if the package has not been updated</returns>
        [HttpPut("{id}")] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateLocation([FromBody] PackageDto packageDto, CancellationToken CancellationToken)
        {
            var result = await _packageService.UpdateAsync(packageDto, CancellationToken);
            return Ok(result); 
        }

        /// <summary>
        /// Functon to delete the package
        /// </summary>
        /// <param name="id">The id of the package</param>
        /// <param name="CancellationToken">The token coming from the Http request</param>
        /// <returns>Ok if the package has been deleted and a bad request if the package has not been deleted</returns>
        [HttpDelete("{id}")]  
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteLocation(string id, CancellationToken CancellationToken)
        {
            var result = await _packageService.DeleteAsync(id, CancellationToken); 
            return Ok(result); 
        }
    }
}
