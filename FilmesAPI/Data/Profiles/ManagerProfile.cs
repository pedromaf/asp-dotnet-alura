using AutoMapper;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;

namespace FilmesAPI.Data.Profiles
{
    public class ManagerProfile : Profile
    {
        public ManagerProfile()
        {
            CreateMap<MTManagerDTO, MTManager>();
            CreateMap<MTManager, ReadMTManagerDTO>();
        }
    }
}
