using ExpeditionService.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ExpeditionService.DataAccess.Interfaces
{
    /// <summary>
    /// The person repository interface to perfom additionals operations on person
    /// </summary>
    public interface IPersonRepository: IBaseRepository<Person>
    {
        /// <summary>
        /// Function to get a person by name
        /// </summary>
        /// <param name="name">the name of the person that we want to get</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="Person"/></returns>
        Task<Person> GetPersonByName(string name, CancellationToken cancellationToken);

        /// <summary>
        /// Function To get a person by phonenumber
        /// </summary>
        /// <param name="phone">the phone number of the person that we want to get</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="Person"/></returns>
        Task<Person> GetPersonByPhoneNumber(string phone, CancellationToken cancellationToken);

    }
}
