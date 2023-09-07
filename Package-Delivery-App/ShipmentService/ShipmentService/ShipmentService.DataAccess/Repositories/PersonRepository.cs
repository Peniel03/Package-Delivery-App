using Microsoft.EntityFrameworkCore;
using ShipmentService.DataAccess.DataContext;
using ShipmentService.DataAccess.Interfaces;
using ShipmentService.DataAccess.Models;
using System.Linq.Expressions;

namespace ShipmentService.DataAccess.Repositories
{
    /// <summary>
    /// Implementation of the repository for crud and additionals operations on the person table
    /// </summary>
    public class PersonRepository : IPersonRepository
    {
        private readonly ShipmentContext _shipmentContext;
        private readonly DbSet<Person> _persons;

        /// <summary>
        /// initialization of a new instance of <see cref="PersonRepository"/>
        /// </summary>
        /// <param name="shipmentContext"></param>
        public PersonRepository(ShipmentContext shipmentContext)
        {
            _shipmentContext = shipmentContext;
            _persons = _shipmentContext.Set<Person>();
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="person">the person that we want to add</param>
        public void AddAsync(Person person)
        {
            _persons.Add(person);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="person">the person that we want to delete</param>
        public void DeleteAsync(Person person)
        {
            _persons.Remove(person);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns>A <see cref="Task"/> taht contains a List of <seealso cref="Person"/> </returns>
        public async Task<List<Person>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _persons
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Person> GetBySomethingAsync(Func<Person, bool> predicate, CancellationToken cancellationToken)
        {
            var query = _persons.AsQueryable(); 
            foreach (var propertyInfo in typeof(Person).GetProperties())
            {
                var parameter = Expression.Parameter(typeof(Person), "x");
                var propertyAccess = Expression.Property(parameter, propertyInfo);
                var value = Expression.Constant(propertyInfo.GetValue(predicate.Target));
                var condition = Expression.Equal(propertyAccess, value);
                var lambda = Expression.Lambda<Func<Person, bool>>(condition, parameter);
                query = query.Where(lambda);
            }
            return await query
                .AsNoTracking()
                .FirstOrDefaultAsync(cancellationToken); 
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <param name="person">the person that we want to update</param>
        public void UpdateAsync(Person person)
        {
            _persons.Update(person);
        }
    }
}
