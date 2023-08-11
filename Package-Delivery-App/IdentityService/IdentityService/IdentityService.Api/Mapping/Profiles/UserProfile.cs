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
                .ForMember(dest => dest.Id, source => source.MapFrom(source => source.Id))
                .ForMember(dest => dest.Email, source => source.MapFrom(source => source.Email))
                .ForMember(dest => dest.PhoneNumber, source => source.MapFrom(source => source.PhoneNumber))
                .ForMember(dest => dest.FirstName, source => source.MapFrom(source => source.FirstName))
                .ForMember(dest => dest.LastName, source => source.MapFrom(source => source.LastName))
                .ForMember(dest => dest.UserName, source => source.MapFrom(source => source.UserName))
                .ReverseMap();

            CreateMap<UserDto, UserCreateRequest>()
                .ForMember(dest => dest.UserName, source => source.MapFrom(source => source.UserName))
                .ForMember(dest => dest.Email, source => source.MapFrom(source => source.Email))
                .ForMember(dest => dest.PhoneNumber, source => source.MapFrom(source => source.PhoneNumber))
                .ForMember(dest => dest.FirstName, source => source.MapFrom(source => source.FirstName))
                .ForMember(dest => dest.LastName, source => source.MapFrom(source => source.LastName))
                .ReverseMap();

            CreateMap<UserDto, UserUpdateRequest>()
                .ForMember(dest => dest.UserName, source => source.MapFrom(source => source.UserName))
                .ForMember(dest => dest.Email, source => source.MapFrom(source => source.Email))
                .ForMember(dest => dest.PhoneNumber, source => source.MapFrom(source => source.PhoneNumber))
                .ForMember(dest => dest.FirstName, source => source.MapFrom(source => source.FirstName))
                .ForMember(dest => dest.LastName, source => source.MapFrom(source => source.LastName))
                .ReverseMap();

            CreateMap<UserDto, UserLoginRequest>()
                .ForMember(dest => dest.Email, source => source.MapFrom(source => source.Email))
           //   .ForMember(dest => dest.Password, source => source.MapFrom(source => source.PasswordHash))
                .ReverseMap();
        }
    }
}
