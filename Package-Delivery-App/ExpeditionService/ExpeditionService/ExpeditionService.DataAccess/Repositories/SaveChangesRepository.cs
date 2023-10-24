using ExpeditionService.DataAccess.DataContext;
using ExpeditionService.DataAccess.Interfaces;

namespace ExpeditionService.DataAccess.Repositories
{
    /// <summary>
    /// implementation of the save changes repository
    /// </summary>
    public class SaveChangesRepository: ISaveChangesRepository
    {
        private readonly ExpeditionContext _expeditionContext;
        /// <summary>
        /// initialization of a new instance of <see cref="SaveChangesRepository"/>
        /// </summary>
        /// <param name="expeditionContext">the database context</param>

        public SaveChangesRepository(ExpeditionContext expeditionContext)
        {
            _expeditionContext = expeditionContext;
        }

        /// <summary>
        /// <inheritdoc/>
        /// </summary>
        /// <returns> A <see cref="Task"/></returns>
        public Task SaveChangesAsync()
        {
           return _expeditionContext.SaveChangesAsync(); 
        }
    }
}
