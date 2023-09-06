using AutoMapper;
using Microsoft.Extensions.Logging;
using ShipmentService.BusinessLogic.DTOs;
using ShipmentService.BusinessLogic.Exceptions;
using ShipmentService.BusinessLogic.Interfaces;
using ShipmentService.DataAccess.Interfaces;
using ShipmentService.DataAccess.Models;
using ShipmentService.DataAccess.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.BusinessLogic.Services
{
    /// <summary>
    /// the implementation of person service to manage persons 
    /// </summary>
    public class PersonService : IPersonService
    {

        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<PersonService> _logger;
        private readonly ISaveChangesRepository _saveChangesRepository;

        /// <summary>
        /// initialization of a new instance of <see cref="PersonService"/>
        /// </summary>
        /// <param name="personRepository"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        /// <param name="saveChangesRepository"></param>
        public PersonService(IPersonRepository personRepository,
            IMapper mapper,
            ILogger<PersonService> logger,
            ISaveChangesRepository saveChangesRepository)
        {
            _personRepository = personRepository;
            _mapper = mapper;
            _logger = logger;
            _saveChangesRepository = saveChangesRepository;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="personDto">the person's datat transfert object</param>
        /// <param name="cancellationToken">the cancelation token</param>
        /// <exception cref="AlreadyExistException">the already exist exception</exception>
        /// <exception cref="ArgumentException">the argument exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="PersonDto"/></returns>
        public async Task<PersonDto> AddAsync(PersonDto personDto, CancellationToken cancellationToken)
        {
            var mappedPerson = _mapper.Map<Person>(personDto);
            var checkedPerson = await _personRepository.GetById(mappedPerson.Id, cancellationToken);

            if (checkedPerson != null)
            {
                _logger.LogError("Error occured while adding the person");
                throw new AlreadyExistException("This person already exist");
            }

            _personRepository.Add(mappedPerson);

            try
            {
                await _saveChangesRepository.SaveChangesAsync();
                _logger.LogInformation("Changes successfully saved in the database");
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"Error occured while adding a person{ex.Message}", ex);
                throw new ArgumentException($"Something went wrong while adding the person {ex.Message}");
            }
            return personDto;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="personDto"></param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="PersonDto"/></returns>
        public async Task<PersonDto> DeleteAsync(PersonDto personDto, CancellationToken cancellationToken)
        {
            var mappedPerson = _mapper.Map<Person>(personDto);
            var checkedPerson = await _personRepository.GetById(mappedPerson.Id, cancellationToken);
            if (checkedPerson == null)
            {
                throw new NotFoundException("The person wasn't found");
            }

            try
            {
                _personRepository.Delete(mappedPerson);
                await _saveChangesRepository.SaveChangesAsync();
                _logger.LogInformation("Changes successfully saved in the database");
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Something went wrong while deleting the person {ex.Message}", ex);
            }
            return personDto; 
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <exception cref="NotFoundException"></exception>
        /// <returns>A <see cref="Task"/>that contains a list of <seealso cref="PersonDto"/></returns>
        public async Task<List<PersonDto>> GetAllAsync(CancellationToken cancellationToken)
        {
            var list = await _personRepository.GetAll(cancellationToken);
            if (list == null)
            {
                throw new NotFoundException("There no registered person yet");
            }

            var listDto = _mapper.Map<List<PersonDto>>(list);

            return listDto;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="personDto">the person's data transfert object</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="PersonDto"/></returns>
        public async Task<PersonDto> GetByIdAsync(PersonDto personDto, CancellationToken cancellationToken)
        {
            var mappedPerson = _mapper.Map<Person>(personDto); 
            var checkedPerson = await _personRepository.GetById(mappedPerson.Id, cancellationToken);
            if (checkedPerson == null)
            {
                throw new NotFoundException("This person wasn't found");
            }
            return _mapper.Map<PersonDto>(checkedPerson);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="personDto">the person's data transfert object</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="PersonDto"/> </returns>
        public async Task<PersonDto> GetPersonByNameAsync(PersonDto personDto, CancellationToken cancellationToken)
        {
            var mappedPerson = _mapper.Map<Person>(personDto);
            var checkedPerson = await _personRepository.GetPersonByName(mappedPerson.Name, cancellationToken);
            if (checkedPerson == null)
            {
                throw new NotFoundException("This person wasn't found");
            }
            return _mapper.Map<PersonDto>(checkedPerson);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="personDto">the person's data transfert object</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="PersonDto"/> </returns>
        public async Task<PersonDto> GetPersonByPhoneNumberAsync(PersonDto personDto, CancellationToken cancellationToken)
        {
            var mappedPerson = _mapper.Map<Person>(personDto);
            var checkedPerson = await _personRepository.GetPersonByPhoneNumber(mappedPerson.Phone, cancellationToken);
            if (checkedPerson == null)
            {
                throw new NotFoundException("This person wasn't found");
            }
            return _mapper.Map<PersonDto>(checkedPerson);
        }


        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="personDto">the person's data transfert object</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="PersonDto"/></returns>
        public async Task<PersonDto> UpdateAsync(PersonDto personDto, CancellationToken cancellationToken)
        {
            var mappedPerson = _mapper.Map<Person>(personDto);
            var checkedPerson = await _personRepository.GetById(mappedPerson.Id, cancellationToken);
            if (checkedPerson == null)
            {
                throw new NotFoundException("This person wasn't found"); 
            }
            try
            {
                _personRepository.Update(mappedPerson);
                await _saveChangesRepository.SaveChangesAsync();
                _logger.LogInformation("Changes successfully saved in the database");
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Something went wrong while adding the person {ex.Message}");
            }

            return personDto; 
        }

    }
}
