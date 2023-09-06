using ShipmentService.BusinessLogic.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShipmentService.BusinessLogic.Interfaces
{
    /// <summary>
    /// The person service interface to perfom additionals operations on person
    /// </summary>
    public interface IPersonService: IBaseService<PersonDto>
    {
        /// <summary>
        /// Function to get a person by name
        /// </summary>
        /// <param name="personDto">the entity that will help us to access the name in the record 
        /// where we want to get the person from</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="PersonDto"/></returns>
        Task<PersonDto> GetPersonByNameAsync(PersonDto personDto, CancellationToken cancellationToken);

        /// <summary>
        /// Function To get a person by phonenumber
        /// </summary>
        /// <param name="personDto">the entity that will help us to access the phone number in the record 
        /// where we want to get the person from</param>
        /// <returns>A <see cref="Task"/> that contains <seealso cref="PersonDto"/></returns>
        Task<PersonDto> GetPersonByPhoneNumberAsync(PersonDto personDto, CancellationToken cancellationToken); 

    }
}
