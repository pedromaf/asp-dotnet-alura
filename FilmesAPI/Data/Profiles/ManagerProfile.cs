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
            CreateMap<MTManager, ReadMTManagerDTO>()
                .ForMember(managerDTO => managerDTO.MovieTheaters, option => option.MapFrom(
                    manager => manager.MovieTheaters.Select(mt => new {
                        mt.Id,
                        mt.Name,
                        mt.AddressId
                    })));
        }
    }
}
