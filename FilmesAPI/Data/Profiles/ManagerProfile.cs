using AutoMapper;
using FilmesAPI.Models.DTOs.Manager;
using FilmesAPI.Models.Entities;

namespace FilmesAPI.Data.Profiles
{
    public class ManagerProfile : Profile
    {
        public ManagerProfile()
        {
            CreateMap<ManagerDTO, Manager>();
            CreateMap<Manager, ReadManagerDTO>();
        }
    }
}
