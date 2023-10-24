using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ShipmentService.BusinessLogic.DTOs;
using ShipmentService.BusinessLogic.Interfaces;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ShipmentService.Api.Controllers
{
    /// <summary>
    /// The person's controller
    /// </summary>
    [Route("api/persons")]
    [ApiController]
    public class PersonController : ControllerBase
    {
        private readonly IPersonService _personService;

        /// <summary>
        /// initialization of a new instance of <see cref="PersonController"/>
        /// </summary>
        /// <param name="personService">the person servcice</param>
        /// <param name="mapper">the mapper</param>
        public PersonController(IPersonService personService)
        {
            _personService = personService;
        }

        /// <summary>
        /// Function to create a new person.
        /// </summary>
        /// <param name="personrequest">The person that we want to create</param>
        /// <param name="CancellationToken">The cancellation token from the http request</param>
        /// <returns>OK if the person has been created or badrequest if not</returns>
        [HttpPost] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePerson([FromBody] PersonDto personDto, CancellationToken CancellationToken)
        {
            var result = await _personService.AddAsync(personDto, CancellationToken);
            return Ok(result); 
        }  

        /// <summary>
        /// Function to get all persons
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the person exists or badrequest if not</returns>
        [HttpGet] 
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPersons(CancellationToken cancellationToken)
        {
            var PersonsList = await _personService.GetAllAsync(cancellationToken);
            return Ok(PersonsList);
        }

        /// <summary>
        /// function to get the person by id
        /// </summary>
        /// <param name="id">the id of the person</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the person exists or badrequest if not</returns>
        [HttpGet("{id}")] 
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPersonById(int id , CancellationToken cancellationToken)
        {
            var result = await _personService.GetByIdAsync(id, cancellationToken); 
            return Ok(result); 
        }

        /// <summary>
        /// Function to get the person by name.
        /// </summary>
        /// <param name="name">the name of the person</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the person exists or badrequest if not</returns>
        [HttpGet("{name}")] 
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPersonByName(string name, CancellationToken cancellationToken) 
        {
            var result = await _personService.GetPersonByNameAsync(name, cancellationToken);  
            return Ok(result); 
        }

        /// <summary>
        /// function to get the person by phone number
        /// </summary>
        /// <param name="phonenumber">the phone number of the person</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the person exists or badrequest if not</returns>
        [HttpGet("{phonenumber}")] 
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPersonByPhoneNumber(string phonenumber, CancellationToken cancellationToken)
        {
            var result = await _personService.GetPersonByPhoneNumberAsync(phonenumber, cancellationToken);
            return Ok(result); 
        }

        /// <summary> 
        /// Function to update the person
        /// </summary>
        /// <param name="id">The id of the person</param>
        /// <param name="PersonDto">The person that we have to update</param>
        /// <param name="CancellationTokenoken">the cancellation token</param>
        /// <returns>OK if the person has been updated and bad request if the package has not been updated</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePerson([FromBody] PersonDto personDto, CancellationToken CancellationToken)
        {
            var result = await _personService.UpdateAsync(personDto, CancellationToken); 
            return Ok(result); 
        }

        /// <summary>
        /// Functon to delete the person
        /// </summary>
        /// <param name="id">The id of the person</param>
        /// <param name="CancellationToken">The token coming from the Http request</param>
        /// <returns>Ok if the person has been deleted and a bad request if the person has not been deleted</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePerson(int id, CancellationToken CancellationToken)
        {
            var result = await _personService.DeleteAsync(id, CancellationToken);
            return Ok(result); 
        } 
    }
}
