using AutoMapper;
using ShipmentService.Api.Request;
using ShipmentService.BusinessLogic.DTOs;
using ShipmentService.DataAccess.Models;

namespace ShipmentService.Api.Mapping.Profiles
{
    /// <summary>
    /// the person profile for mapping
    /// </summary>
    public class PersonProfile: Profile
    {
        /// <summary>
        /// Initialization of a new instance of <see cref="PersonProfile"/>
        /// </summary>
        public PersonProfile()
        {
            CreateMap<PersonDto, Person>()
                .ReverseMap();
            CreateMap<PersonRequest,PersonDto>()
                .ReverseMap();

        }
    }
}
