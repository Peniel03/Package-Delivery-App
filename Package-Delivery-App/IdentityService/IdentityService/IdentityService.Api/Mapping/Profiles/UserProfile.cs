using AutoMapper;
using IdentityService.Api.Request;
using IdentityService.BusinessLogic.DTOs;
using IdentityService.DataAccess.Models;

namespace IdentityService.Api.Mapping.Profiles
{
    /// <summary>
    /// The profile for mappinng users
    /// </summary>
    public class UserProfile:Profile
    {
        /// <summary>
        /// initializes a new instance of <see cref="UserProfile"/>
        /// </summary>
        public UserProfile()
        {

            CreateMap<UserDto, User>() 
                .ReverseMap();

            CreateMap<AuthorizeUserDto, User>()
                .ReverseMap();

            CreateMap<UserUpdatePasswordDto, User>()
               .ReverseMap();

            CreateMap<DeleteUserDto, User>()
                .ReverseMap();
        }
    }
}
