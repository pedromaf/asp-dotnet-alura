using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Models.DTOs;
using UsuariosAPI.Models.Entities;

namespace UsuariosAPI.Data.Profiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<CreateUserDTO, User>();
            CreateMap<User, IdentityUser<int>>();
        }
    }
}
