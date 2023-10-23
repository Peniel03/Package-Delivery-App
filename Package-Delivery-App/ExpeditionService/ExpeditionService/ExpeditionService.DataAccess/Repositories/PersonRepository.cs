using ExpeditionService.DataAccess.DataContext;
using ExpeditionService.DataAccess.Interfaces;
using ExpeditionService.DataAccess.Models;
using MongoFramework;
using MongoFramework.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ExpeditionService.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of the repository for crud and additionals operations on the person table
    /// </summary>
    public class PersonRepository: IPersonRepository
    {
        private readonly ExpeditionContext _expeditionContext;
        private readonly MongoDbSet<Person> _persons;

        /// <summary>
        /// initialization of a new instance of <see cref="PersonRepository"/>
        /// </summary>
        /// <param name="ExpeditionContext"></param>
        public PersonRepository(ExpeditionContext expeditionContext) 
        {
            _expeditionContext = expeditionContext;
            _persons = (MongoDbSet<Person>?)_expeditionContext.Set<Person>();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="person">the person that we want to add</param>
        public void Add(Person person)
        {
            _persons.Add(person);
         }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="person">the person that we want to delete</param>
        public void Delete(Person person) 
        {
            _persons.Remove(person);
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
        /// <param name="id">the id of the person that wew want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns> a <see cref="Task"/> that contains <seealso cref="Person"/></returns>
        public Task<Person> GetById(string id, CancellationToken cancellationToken)
        {
            return _persons
                       .AsNoTracking()
                       .FirstOrDefaultAsync(x => x.Id == id);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="name">the name of the personn that we want to get</param>
        /// <param name="cancellationToken">the cancellation robuste</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="Person"/></returns>
        public Task<Person> GetPersonByName(string name, CancellationToken cancellationToken)
        {
          return  _persons
                    .AsNoTracking()
                    .FirstOrDefaultAsync(x => x.Name == name);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="phone">the phone number of the person that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="Person"/></returns>
        public Task<Person> GetPersonByPhoneNumber(string phone, CancellationToken cancellationToken)
        {
            return _persons
                       .AsNoTracking()
                       .FirstOrDefaultAsync(x => x.Phone == phone);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="person">the person that we want to update</param>
        public void Update(Person person)
        {
            _persons.Update(person);
        }
    }
}
