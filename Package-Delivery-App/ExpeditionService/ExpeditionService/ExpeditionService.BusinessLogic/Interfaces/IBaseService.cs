namespace ExpeditionService.BusinessLogic.Interfaces
{
    /// <summary>
    /// The base service interface to perform crup operation in all services
    /// </summary>
    /// <typeparam name="T">will be replaced by the given class</typeparam>
    public interface IBaseService<T>
    {
        /// <summary>
        /// Function to add a new record of a given entity
        /// </summary>
        /// <param name="entity">the entity where we want to add the record</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="T"/></returns>
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// Function to update a record of a given entity
        /// </summary>
        /// <param name="entity">the entity wheere we want to update the record</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>that contains <seealso cref="T"/></returns>
        Task<T> UpdateAsync(T entity, CancellationToken cancellationToken);

        /// <summary>
        /// Function to delete a record of a given entity
        /// </summary>
        /// <param name="id">the id the entity where we want to delete the record</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>That contains <seealso cref="T"/></returns>
        Task<T> DeleteAsync(string id, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get a record of a given entity by id
        /// </summary>
        /// <param name="entity">the entity that will help us to access the id 
        /// of the record that we want to get</param>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>A <see cref="Task"/>That contains <seealso cref="T"/></returns>
        Task<T> GetByIdAsync(string id, CancellationToken cancellationToken);

        /// <summary>
        /// Function to get all records of a given entity
        /// </summary>
        /// <param name="cancellationToken">the cancellation</param>
        /// <returns>A <see cref="Task"/>that contains a list of <seealso cref="T"/></returns>
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);
    } 
}
