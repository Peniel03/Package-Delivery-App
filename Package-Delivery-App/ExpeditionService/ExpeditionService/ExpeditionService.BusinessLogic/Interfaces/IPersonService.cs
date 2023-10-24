using ExpeditionService.BusinessLogic.DTOs;

namespace ExpeditionService.BusinessLogic.Interfaces
{
    /// <summary>
    /// The person service interface to perfom additionals operations on person
    /// </summary>
    public interface IPersonService: IBaseService<PersonDto> 
    {
        /// <summary>
        /// Function to get a person by name
        /// </summary>
        /// <param name="name"> the name of the person that we want to get
        /// <returns>A <see cref="Task"/> that contains <seealso cref="PersonDto"/></returns>
        Task<PersonDto> GetPersonByNameAsync(string name, CancellationToken cancellationToken);

        /// <summary>
        /// Function To get a person by phonenumber
        /// </summary>
        /// <param name="phonenumber">the phone number of the person that we want to get
        /// <returns>A <see cref="Task"/> that contains <seealso cref="PersonDto"/></returns>
        Task<PersonDto> GetPersonByPhoneNumberAsync(string phonenumber, CancellationToken cancellationToken);
    }
}
