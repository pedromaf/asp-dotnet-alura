using AutoMapper;
using FilmesAPI.Models.DTOs.MovieTheater;
using FilmesAPI.Models.Entities;

namespace FilmesAPI.Data.Profiles
{
    public class MovieTheaterProfile : Profile
    {
        public MovieTheaterProfile()
        {
            CreateMap<MovieTheaterDTO, MovieTheater>();
            CreateMap<MovieTheater, ReadMovieTheaterDTO>();
        }
    }
}
