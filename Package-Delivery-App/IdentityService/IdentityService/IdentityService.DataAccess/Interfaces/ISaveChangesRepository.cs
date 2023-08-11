using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IdentityService.DataAccess.Interfaces
{
    /// <summary>
    ///  Repository for saving the changes
    /// </summary>
    public interface ISaveChangesRepository
    {
        /// <summary>
        /// Function to save changes to the database
        /// </summary>
        /// <param name="cancellationToken">The cancellation cancellationToken</param>
        /// <returns><see cref="Task"/></returns>
        Task SaveChangesAsync(CancellationToken cancellationToken);

    }
}
