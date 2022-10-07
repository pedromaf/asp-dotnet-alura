using AutoMapper;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;

namespace FilmesAPI.Data.Profiles
{
    public class MovieTheaterProfile : Profile
    {
        public MovieTheaterProfile()
        {
            CreateMap<MovieTheaterDTO, MovieTheater>();
            CreateMap<MovieTheater, ReadMovieTheaterDTO>()
                .ForMember(dto => dto.Manager, opt => opt.MapFrom(
                    mt => new
                    {
                        mt.Manager.Id,
                        mt.Manager.Name
                    }))
                .ForMember(dto => dto.Address, opt => opt.MapFrom(
                    mt => new
                    {
                        mt.Address.Id,
                        mt.Address.City,
                        mt.Address.District,
                        mt.Address.Street,
                        mt.Address.Number,
                        mt.Address.PostalCode
                    }))
                .ForMember(dto => dto.Sessions, opt => opt.MapFrom(
                    mt => mt.Sessions.Select(s => new
                    {
                        s.Id,
                        s.MovieId,
                        s.Start,
                        s.End
                    })));
        }
    }
}
