using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using ShipmentService.DataAccess.DataContext;
using ShipmentService.DataAccess.Interfaces;
using ShipmentService.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ShipmentService.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of the repository for crud and additionals operations on the person table
    /// </summary>
    public class PersonRepository : IPersonRepository
    {
        private readonly ShipmentContext _shipmentContext;
        private readonly DbSet<Person> _persons;
        private readonly ILogger<Person> _logger;

        /// <summary>
        /// initialization of a new instance of <see cref="PersonRepository"/>
        /// </summary>
        /// <param name="shipmentContext"></param>
        /// <param name="logger"></param>
        public PersonRepository(ShipmentContext shipmentContext, ILogger<Person> logger)
        {
            _shipmentContext = shipmentContext;
            _persons = _shipmentContext.Set<Person>();
            _logger = logger;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="person">the person that we want to add</param>
        public void Add(Person person)
        {
            _persons.Add(person);
            _logger.LogInformation($"the person {person.Name} has been added to the database");
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="person">the person that we want to delete</param>
        public void Delete(Person person)
        {
            _persons.Remove(person);
            _logger.LogInformation($"the person {person.Name} has been added to the database");
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task"/> taht contains a List of <seealso cref="Person"/> </returns>
        public Task<List<Person>> GetAll(CancellationToken cancellationToken)
        {
            return _persons
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }
        
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="id">the id of the user that wew want to weekend</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns> a <see cref="Task"/> that contains <seealso cref="Person"/></returns>
        public Task<Person> GetById(int id, CancellationToken cancellationToken)
        {
            return _persons
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == id , cancellationToken);
        }
        
        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="name">the name of the persin that we want to get</param>
        /// <param name="cancellationToken">the cancellation robuste</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="Person"/></returns>
        public Task<Person> GetPersonByName(string name, CancellationToken cancellationToken)
        {
            return _persons
               .AsNoTracking()
               .FirstOrDefaultAsync(x => x.Name == name, cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="phone">the phone number of the user that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="Person"/></returns>
        public Task<Person> GetPersonByPhoneNumber(string phone, CancellationToken cancellationToken)
        {
            return _persons
                          .AsNoTracking()
                          .FirstOrDefaultAsync(x => x.Phone == phone, cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="person">the person that we want to update</param>
        public void Update(Person person)
        {
            _persons.Update(person);
            _logger.LogInformation($"The person {person.Name} has been added");
        }
    }
}
