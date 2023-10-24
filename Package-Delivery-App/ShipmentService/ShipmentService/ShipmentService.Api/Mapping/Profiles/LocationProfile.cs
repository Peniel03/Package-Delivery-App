using AutoMapper;
using ShipmentService.BusinessLogic.DTOs;
using ShipmentService.DataAccess.Models;

namespace ShipmentService.Api.Mapping.Profiles
{
    /// <summary>
    /// The location profile for mapping 
    /// </summary>
    public class LocationProfile: Profile
    {
        /// <summary>
        /// initialization of a new instance of <see cref="LocationProfile"/>
        /// </summary>
        public LocationProfile()
        {
            CreateMap<LocationDto, Location>()
                .ReverseMap();
        }
    }
}
