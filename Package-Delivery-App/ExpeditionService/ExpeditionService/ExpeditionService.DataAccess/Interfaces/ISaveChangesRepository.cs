using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionService.DataAccess.Interfaces
{
    /// <summary>
    /// The save changes repository
    /// </summary>
    public interface ISaveChangesRepository
    {
        /// <summary>
        /// Function to save the changes to the database 
        /// </summary>
        /// <returns>A <see cref="Task"/></returns>
        Task SaveChangesAsync();
    }
}
