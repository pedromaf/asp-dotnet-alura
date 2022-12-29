using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Models.Enums;
using FilmesAPI.Repositories.Interfaces;

namespace FilmesAPI.Services
{
    public class MovieSessionService
    {
        private readonly IMovieSessionRepository _repository;
        private readonly IMapper _mapper;

        public MovieSessionService(IMovieSessionRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        public ReadMovieSessionDTO Create(MovieSessionDTO movieSessionDTO)
        {
            MovieSession movieSession = _mapper.Map<MovieSession>(movieSessionDTO);

            _repository.Add(movieSession);

            ReadMovieSessionDTO readDTO = _mapper.Map<ReadMovieSessionDTO>(movieSession);

            return readDTO;
        } 

        public List<ReadMovieSessionDTO> GetAll()
        {
            List<MovieSession> movieSessionsList = _repository.GetAll();
            List<ReadMovieSessionDTO> readDTOList = _mapper.Map<List<ReadMovieSessionDTO>>(movieSessionsList);

            return readDTOList;
        }

        public ReadMovieSessionDTO GetById(int id)
        {
            MovieSession movieSession = _repository.GetById(id);
        
            if(movieSession == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIESESSION);
            }

            ReadMovieSessionDTO readDTO = _mapper.Map<ReadMovieSessionDTO>(movieSession);

            return readDTO;
        }

        public ReadMovieSessionDTO Update(int id, MovieSessionDTO movieSessionDTO)
        {
            MovieSession movieSession = _repository.GetById(id);

            if (movieSession == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIESESSION);
            }

            _mapper.Map(movieSessionDTO, movieSession);

            _repository.Update(movieSession);

            ReadMovieSessionDTO readDTO = _mapper.Map<ReadMovieSessionDTO>(movieSession);

            return readDTO;
        }

        public void Delete(int id)
        {
            MovieSession movieSession = _repository.GetById(id);

            if(movieSession == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIESESSION);
            }

            _repository.Delete(movieSession);
        }
    }
}
