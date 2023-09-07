using AutoMapper;
using ExpeditionService.Api.Request;
using ExpeditionService.BusinessLogic.DTOs;
using ExpeditionService.DataAccess.Models;

namespace ExpeditionService.Api.Mapping.Profiles
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
