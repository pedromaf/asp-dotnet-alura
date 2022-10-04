using AutoMapper;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;

namespace FilmesAPI.Data.Profiles
{
    public class MovieProfile : Profile
    {
        public MovieProfile()
        {
            CreateMap<MovieDTO, Movie>();
            CreateMap<Movie, ReadMovieDTO>();
        }
    }
}
