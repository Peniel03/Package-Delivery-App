using AutoMapper;
using ExpeditionService.Api.Request;
using ExpeditionService.BusinessLogic.DTOs;
using ExpeditionService.DataAccess.Models;

namespace ExpeditionService.Api.Mapping.Profiles
{
    /// <summary>
    /// the shipment profile for mapping
    /// </summary>
    public class ShipmentProfile:Profile
    {
        // <summary>
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
