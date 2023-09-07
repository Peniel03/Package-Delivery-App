using AutoMapper;
using ExpeditionService.Api.Request;
using ExpeditionService.BusinessLogic.DTOs;
using ExpeditionService.DataAccess.Models;

namespace ExpeditionService.Api.Mapping.Profiles
{
    /// <summary>
    /// the person profile for mapping
    /// </summary>
    public class PersonProfile:Profile
    {
        /// <summary>
        /// Initialization of a new instance of <see cref="PersonProfile"/>
        /// </summary>
        public PersonProfile()
        {
            CreateMap<PersonDto, Person>()
                .ReverseMap();
            CreateMap<PersonRequest, PersonDto>()
                .ReverseMap();
        }
    }
}
