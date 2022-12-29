using AutoMapper;
using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Models.Enums;
using FilmesAPI.Repositories.Interfaces;

namespace FilmesAPI.Services
{
    public class MoviesService
    {
        private readonly IMovieRepository _repository;
        private readonly IMapper _mapper;

        public MoviesService(IMovieRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ReadMovieDTO Create(MovieDTO movieDTO)
        {
            Movie movie = _mapper.Map<Movie>(movieDTO);

            _repository.Add(movie);

            ReadMovieDTO readDTO = _mapper.Map<ReadMovieDTO>(movie);

            return readDTO;
        }

        public List<ReadMovieDTO> GetAll(int? ageRating)
        {
            List<Movie> movieList = _repository.GetAll();

            if (ageRating != null)
            {
                movieList.RemoveAll(movie => movie.AgeRating > ageRating);
            }

            List<ReadMovieDTO> readDTOList = _mapper.Map<List<ReadMovieDTO>>(movieList);

            return readDTOList;
        }

        public ReadMovieDTO GetById(int id)
        {
            Movie movie = _repository.GetById(id);
            
            if(movie == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIE);
            }

            ReadMovieDTO readDTO = _mapper.Map<ReadMovieDTO>(movie);

            return readDTO;
        }

        public ReadMovieDTO Update(int id, MovieDTO movieDTO)
        {
            Movie movie = _repository.GetById(id);

            if (movie == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIE);
            }

            _mapper.Map(movieDTO, movie);

            _repository.Update(movie);

            ReadMovieDTO readDTO = _mapper.Map<ReadMovieDTO>(movie);

            return readDTO;
        }

        public void Delete(int id)
        {
            Movie movie = _repository.GetById(id);

            if (movie == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIE);
            }

            _repository.Delete(movie);
        }
    }
}
