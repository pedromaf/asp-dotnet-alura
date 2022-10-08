using AutoMapper;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;

namespace FilmesAPI.Data.Profiles
{
    public class MovieSessionProfile : Profile
    {
        public MovieSessionProfile()
        {
            CreateMap<MovieSessionDTO, MovieSession>();
            CreateMap<MovieSession, ReadMovieSessionDTO>()
                .ForMember(dto => dto.Movie, opt => opt.MapFrom(
                    ms => new
                    {
                        ms.Movie.Id,
                        ms.Movie.Name,
                        ms.Movie.Director,
                        ms.Movie.Genre,
                        ms.Movie.Description,
                        ms.Movie.ReleaseDate,
                        ms.Movie.AgeRating
                    }))
                .ForMember(dto => dto.MovieTheater, opt => opt.MapFrom(
                    mt => new
                    {
                        mt.MovieTheater.Id,
                        mt.MovieTheater.Name,
                        mt.MovieTheater.AddressId
                    }));
        }
    }
}
