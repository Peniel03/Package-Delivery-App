

namespace ShipmentService.DataAccess.Interfaces
{
    /// <summary>
    /// The base repository interface to perform crup operation in all repositories
    /// </summary>
    /// <typeparam name="T">will be replaced by the given class</typeparam>
    public interface IBaseRepository<T>
    {
        /// <summary>
        /// Function to create a new record of a given entity
        /// </summary>
        /// <param name="entity">the entity where we want to add the record</param>
        void Add(T entity);

        /// <summary>
        /// Function to update a record of a given entity
        /// </summary>
        /// <param name="entity">the entity where we want to update the record</param>
        void Update(T entity);

        /// <summary>
        /// Function to delete a record of a given entity
        /// </summary>
        /// <param name="entity">the entity where we want to delete the record</param>
        void Delete(T entity);

        /// <summary>
        /// Function to get a record of a given entity by id
        /// </summary>
        /// <param name="id">the id of the record that we want to get</param>
        /// <returns>A <see cref="Task"/> that contains the given entity <seealso cref="T"/></returns>
        Task<T> GetById(int id , CancellationToken cancellationToken);

        /// <summary>
        /// Function to get all records of a given entity
        /// </summary>
        /// <returns>A List of <see cref="T"/></returns>
        Task<List<T>> GetAll(CancellationToken cancellationToken);

    }
}
