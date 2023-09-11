using AutoMapper;
using ShipmentService.Api.Request;
using ShipmentService.BusinessLogic.DTOs;
using ShipmentService.DataAccess.Models;

namespace ShipmentService.Api.Mapping.Profiles
{
    /// <summary>
    /// the package profile for mapping
    /// </summary>
    public class PackageProfile:Profile
    {
        /// <summary>
        /// initialization of a new instance of <see cref="PackageProfile"/>
        /// </summary>
        public PackageProfile()
        {
            CreateMap<PackageDto, Package>()
                .ReverseMap();

            CreateMap<PackageRequest, PackageDto>()
                .ReverseMap();

        }

    }
}
