using AutoMapper;
using IdentityService.DataAccess.Models;
using System.Security.Claims;

namespace IdentityService.Api.Mapping.Profiles
{
    /// <summary>
    /// The profile for mapping claims
    /// </summary>
    public class ClaimProfile: Profile
    {
        /// <summary>
        /// Initializes a new instance of <see cref="ClaimProfile"/>
        /// </summary>
        public ClaimProfile()
        {
            CreateMap<Claim, UserClaim>()
                 .ReverseMap();
        }
    }
}
