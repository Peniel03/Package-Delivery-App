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
    /// The person's controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class PersonController : ControllerBase
    {

        private readonly IPersonService _personService;
        private readonly IMapper _mapper;

        /// <summary>
        /// initialization of a new instance of <see cref="PersonController"/>
        /// </summary>
        /// <param name="personService">the person servcice</param>
        /// <param name="mapper">the mapper</param>
        public PersonController(IPersonService personService, IMapper mapper)
        {
            _personService = personService;
            _mapper = mapper;
        }

        /// <summary>
        /// Function to create a new person.
        /// </summary>
        /// <param name="personrequest">The person that we want to create</param>
        /// <param name="CancellationToken">The cancellation token from the http request</param>
        /// <returns>OK if the person has been created or badrequest if not</returns>
        [HttpPost("create-person/{packagerequest,CancellationToken}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreatePerson([FromBody] PersonRequest personrequest, CancellationToken CancellationToken)
        {
            var result = await _personService.AddAsync(_mapper.Map<PersonDto>(personrequest), CancellationToken);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("Could not create the person");
        }  

        /// <summary>
        /// Function to get all persons
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the person exists or badrequest if not</returns>
        [HttpGet("get-all-persons/{CancellationToken}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetAllPersons(CancellationToken cancellationToken)
        {
            var PersonsList = await _personService.GetAllAsync(cancellationToken);

            if (PersonsList == null)
            {
                return BadRequest("There is no persons");
            }

            return Ok(PersonsList);
        }

        /// <summary>
        /// function to get the person by id
        /// </summary>
        /// <param name="id">the id of the person</param>
        /// <param name="personrequest">the person that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the person exists or badrequest if not</returns>
        [HttpGet("get-person-by-id/{id,personrequest,cancellationToken}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPersonById(int id, [FromBody] PersonRequest personrequest, CancellationToken cancellationToken)
        {
            var person = _mapper.Map<PersonDto>(personrequest);
            person.Id = id;
            var result = await _personService.GetByIdAsync(person, cancellationToken);

            if (result == null)
            {
                return BadRequest("This person does not exist");
            }

            return Ok(person);
        }


        /// <summary>
        /// Function to get the person by name.
        /// </summary>
        /// <param name="name">the name of the person</param>
        /// <param name="packagerequest">the package that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the package exists or badrequest if not</returns>
        [HttpGet("get-person-by-name/{name,personrequest,cancellationToken}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPersonByName(string name, [FromBody] PersonRequest personrequest, CancellationToken cancellationToken) 
        {
            var person = _mapper.Map<PersonDto>(personrequest);
            person.Name = name;
            var result = await _personService.GetPersonByNameAsync(person, cancellationToken); 

            if (result == null)
            {
                return BadRequest("This person does not exist");
            }

            return Ok(person); 
        }


        /// <summary>
        /// function to get the person by phone number
        /// </summary>
        /// <param name="phonenumber">the phone number of the person</param>
        /// <param name="personrequest">the person that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>OK if the person exists or badrequest if not</returns>
        [HttpGet("get-person-by-phonenumber/{phonenumber,personrequest,cancellationToken}")]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetPersonByPhoneNumber(string phonenumber, [FromBody] PersonRequest personrequest, CancellationToken cancellationToken)
        {
            var person = _mapper.Map<PersonDto>(personrequest);
            person.Phone = phonenumber;
            var result = await _personService.GetPersonByPhoneNumberAsync(person, cancellationToken);

            if (result == null)
            {
                return BadRequest("This person does not exist");
            }

            return Ok(person);
        }



        /// <summary>
        /// Function to update the person
        /// </summary>
        /// <param name="id">The id of the person</param>
        /// <param name="personrequest">The person that we have to update</param>
        /// <param name="CancellationTokenoken">the cancellation token</param>
        /// <returns>OK if the person has been updated and bad request if the package has not been updated</returns>
        [HttpPut("update-person/{id,personrequest,CancellationToken}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdatePerson(int id, [FromBody] PersonRequest personrequest, CancellationToken CancellationToken)
        {
            var person = _mapper.Map<PersonDto>(personrequest);
            person.Id = id;

            var result = await _personService.UpdateAsync(person, CancellationToken);

            if (result != null) 
            {
                return Ok(result);
            }

            return BadRequest("Could not update the person");
        }

        /// <summary>
        /// Functon to delete the person
        /// </summary>
        /// <param name="id">The id of the person</param>
        /// <param name="CancellationToken">The token coming from the Http request</param>
        /// <returns>Ok if the person has been deleted and a bad request if the person has not been deleted</returns>
        [HttpDelete("delete-person/{id,personrequest,CancellationToken}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeletePerson(int id, [FromBody] PersonRequest personrequest, CancellationToken CancellationToken)
        {
            var person = _mapper.Map<PersonDto>(personrequest);
            person.Id = id;

            var result = await _personService.DeleteAsync(person, CancellationToken);

            if (result != null)
            {
                return Ok(result);
            }

            return BadRequest("Could not delete the person");
        }

    }
}
