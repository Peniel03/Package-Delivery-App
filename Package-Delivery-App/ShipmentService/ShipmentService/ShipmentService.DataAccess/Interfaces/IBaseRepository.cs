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
        void AddAsync(T entity);

        /// <summary>
        /// Function to update a record of a given entity
        /// </summary>
        /// <param name="entity">the entity where we want to update the record</param>
        void UpdateAsync(T entity);

        /// <summary>
        /// Function to delete a record of a given entity
        /// </summary>
        /// <param name="entity">the entity where we want to delete the record</param>
        void DeleteAsync(T entity);

        /// <summary>
        /// Function to get all records of a given entity
        /// </summary>
        /// <returns>A List of <see cref="T"/></returns>
        Task<List<T>> GetAllAsync(CancellationToken cancellationToken);

        /// <summary>
        /// function to get a record of a given entity by a given predicate
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<T> GetBySomethingAsync(Func<T, bool> predicate, CancellationToken cancellationToken);
    }
}
