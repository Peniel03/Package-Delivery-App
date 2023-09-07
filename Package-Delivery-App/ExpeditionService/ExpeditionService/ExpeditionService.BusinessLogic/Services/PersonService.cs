using AutoMapper;
using ExpeditionService.BusinessLogic.DTOs;
using ExpeditionService.BusinessLogic.Exceptions;
using ExpeditionService.BusinessLogic.Interfaces;
using ExpeditionService.DataAccess.Interfaces;
using ExpeditionService.DataAccess.Models;

namespace ExpeditionService.BusinessLogic.Services
{
    /// <summary>
    /// the implementation of person service to manage persons 
    /// </summary>
    public class PersonService: IPersonService
    {
        private readonly IPersonRepository _personRepository;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _logger;
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
            ILoggerManager logger,
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
            var checkedPerson = await _personRepository.GetBySomethingAsync(x => x.Id == mappedPerson.Id, cancellationToken);
            if (checkedPerson != null)
            {
                _logger.LogError("Error occured while adding the person");
                throw new AlreadyExistException("This person already exist");
            }
            _personRepository.AddAsync(mappedPerson);
            try
            {
                await _saveChangesRepository.SaveChangesAsync();
                _logger.LogInfo("Changes successfully saved in the database");
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occured while adding a person{ex.Message}");
                throw new ArgumentException($"Something went wrong while adding the person {ex.Message}");
            }
            return personDto;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="id">the id of the person that we want to delete</param>
        /// <param name="cancellationToken"></param>
        /// <exception cref="NotFoundException"></exception>
        /// <exception cref="ArgumentException"></exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="PersonDto"/></returns>
        public async Task<PersonDto> DeleteAsync(string id, CancellationToken cancellationToken)
        {
            PersonDto personDto = new PersonDto();
            var mappedPerson = _mapper.Map<Person>(personDto);
            mappedPerson.Id = id;
            var checkedPerson = await _personRepository.GetBySomethingAsync(x => x.Id == mappedPerson.Id, cancellationToken);
            if (checkedPerson == null)
            {
                throw new NotFoundException("The person wasn't found");
            } 
            try
            {
                _personRepository.DeleteAsync(mappedPerson);
                await _saveChangesRepository.SaveChangesAsync();
                _logger.LogInfo("Changes successfully saved in the database");
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
            var list = await _personRepository.GetAllAsync(cancellationToken);
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
        /// <param name="id">the id of the person that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="PersonDto"/></returns>
        public async Task<PersonDto> GetByIdAsync(string id, CancellationToken cancellationToken)
        {
            PersonDto personDto = new PersonDto();
            var mappedPerson = _mapper.Map<Person>(personDto);
            mappedPerson.Id = id;
            var checkedPerson = await _personRepository.GetBySomethingAsync(x => x.Id == mappedPerson.Id, cancellationToken);
            if (checkedPerson == null)
            {
                throw new NotFoundException("This person wasn't found");
            }
            return _mapper.Map<PersonDto>(checkedPerson);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="name">the name of the person that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="PersonDto"/> </returns>
        public async Task<PersonDto> GetPersonByNameAsync(string name, CancellationToken cancellationToken)
        {
            PersonDto personDto = new PersonDto();
            var mappedPerson = _mapper.Map<Person>(personDto);
            mappedPerson.Name = name;
            var checkedPerson = await _personRepository.GetBySomethingAsync(x => x.Name == mappedPerson.Name, cancellationToken);
            if (checkedPerson == null)
            {
                throw new NotFoundException("This person wasn't found");
            }
            return _mapper.Map<PersonDto>(checkedPerson);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="phonenumber">the phonenumber of the person that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <exception cref="NotFoundException">the not found exception</exception>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="PersonDto"/> </returns>
        public async Task<PersonDto> GetPersonByPhoneNumberAsync(string phonenumber, CancellationToken cancellationToken)
        {
            PersonDto personDto = new PersonDto();
            var mappedPerson = _mapper.Map<Person>(personDto);
            mappedPerson.Phone = phonenumber; 
            var checkedPerson = await _personRepository.GetBySomethingAsync(x => x.Phone == mappedPerson.Phone, cancellationToken);
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
            var checkedPerson = await _personRepository.GetBySomethingAsync(x => x.Id == mappedPerson.Id, cancellationToken);
            if (checkedPerson == null)
            {
                throw new NotFoundException("This person wasn't found");
            } 
            try
            {
                _personRepository.UpdateAsync(mappedPerson);
                await _saveChangesRepository.SaveChangesAsync();
                _logger.LogInfo("Changes successfully saved in the database");
            }
            catch (Exception ex)
            {
                throw new ArgumentException($"Something went wrong while adding the person {ex.Message}");
            }
            return personDto;
        }
    }
}
