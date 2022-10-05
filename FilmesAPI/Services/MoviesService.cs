using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Services
{
    public class MoviesService
    {
        private FilmesContext _DbContext;
        private IMapper _mapper;

        public MoviesService(FilmesContext context, IMapper mapper)
        {
            _DbContext = context;
            _mapper = mapper;
        }

        public Movie Create(MovieDTO movieDTO)
        {
            Movie movie = _mapper.Map<Movie>(movieDTO);

            _DbContext.Movies.Add(movie);
            _DbContext.SaveChanges();

            return movie;
        }

        public List<Movie> GetAll()
        {
            return _DbContext.Movies.ToList();
        }

        public ReadMovieDTO GetById(int id)
        {
            Movie movie = _DbContext.Movies.FirstOrDefault(m => m.Id == id);
            
            if(movie == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIE);
            }

            ReadMovieDTO movieDTO = _mapper.Map<ReadMovieDTO>(movie);

            return movieDTO;
        }

        public Movie Update(int id, MovieDTO movieDTO)
        {
            Movie movie = _DbContext.Movies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIE);
            }

            _mapper.Map(movieDTO, movie);

            _DbContext.SaveChanges();

            return movie;
        }

        public void Delete(int id)
        {
            Movie movie = _DbContext.Movies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIE);
            }

            _DbContext.Remove(movie);
            _DbContext.SaveChanges();
        }
    }
}
