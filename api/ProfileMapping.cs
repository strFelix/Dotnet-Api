using AutoMapper;
using api.Models;
using api.Dtos;


namespace api
{
    public class ProfileMapping : Profile
    {
        public ProfileMapping()
        {
            CreateMap<UserDto, User>();
        }
    }
}