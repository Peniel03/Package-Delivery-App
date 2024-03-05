using AutoMapper;
using ShipmentService.Api.Request;
using ShipmentService.BusinessLogic.DTOs;
using ShipmentService.DataAccess.Models;

namespace ShipmentService.Api.Mapping.Profiles
{
    /// <summary>
    /// the shipment profile for mapping
    /// </summary>
    public class ShipmentProfile: Profile
    {
        /// <summary>
        /// initialization of a new instance of <see cref="ShipmentProfile"/>
        /// </summary>
        public ShipmentProfile()
        {
            CreateMap<ShipmentDto, Shipment>()
                 .ReverseMap();

            CreateMap<ShipmentCreateRequest, ShipmentDto>()
                    .ReverseMap();

            CreateMap<ShipmentUpdateRequest, ShipmentDto>()
                   .ReverseMap();

        }
    }
}
