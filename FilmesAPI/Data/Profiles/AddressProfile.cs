using AutoMapper;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;

namespace FilmesAPI.Data.Profiles
{
    public class AddressProfile : Profile
    {
        public AddressProfile()
        {
            CreateMap<AddressDTO, Address>();
            CreateMap<Address, ReadAddressDTO>()
                .ForMember(dto => dto.MovieTheater, opt => opt.MapFrom(
                    address => new { 
                        address.MovieTheater.Id,
                        address.MovieTheater.Name,
                        address.MovieTheater.ManagerId
                    }));
        }
    }
}
