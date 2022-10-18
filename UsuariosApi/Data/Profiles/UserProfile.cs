using AutoMapper;
using UsuariosAPI.Models.DTOs;
using UsuariosAPI.Models.Entities;

namespace UsuariosAPI.Data.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDTO, User>();
        }
    }
}
