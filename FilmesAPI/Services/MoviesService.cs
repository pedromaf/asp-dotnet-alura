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
        private readonly FilmesContext _DbContext;
        private readonly IMapper _mapper;

        public MoviesService(FilmesContext context, IMapper mapper)
        {
            _DbContext = context;
            _mapper = mapper;
        }

        public ReadMovieDTO Create(MovieDTO movieDTO)
        {
            Movie movie = _mapper.Map<Movie>(movieDTO);

            _DbContext.Movies.Add(movie);
            _DbContext.SaveChanges();

            ReadMovieDTO readDTO = _mapper.Map<ReadMovieDTO>(movie);

            return readDTO;
        }

        public List<ReadMovieDTO> GetAll()
        {
            List<Movie> movieList = _DbContext.Movies.ToList();
            List<ReadMovieDTO> readDTOList = _mapper.Map<List<ReadMovieDTO>>(movieList);

            return readDTOList;
        }

        public ReadMovieDTO GetById(int id)
        {
            Movie movie = _DbContext.Movies.FirstOrDefault(m => m.Id == id);
            
            if(movie == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIE);
            }

            ReadMovieDTO readDTO = _mapper.Map<ReadMovieDTO>(movie);

            return readDTO;
        }

        public ReadMovieDTO Update(int id, MovieDTO movieDTO)
        {
            Movie movie = _DbContext.Movies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIE);
            }

            _mapper.Map(movieDTO, movie);

            _DbContext.SaveChanges();

            ReadMovieDTO readDTO = _mapper.Map<ReadMovieDTO>(movie);

            return readDTO;
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
