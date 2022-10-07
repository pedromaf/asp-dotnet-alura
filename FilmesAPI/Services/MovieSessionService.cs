using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Models.Enums;

namespace FilmesAPI.Services
{
    public class MovieSessionService
    {
        private readonly FilmesContext _DbContext;
        private readonly IMapper _mapper;

        public MovieSessionService(FilmesContext context, IMapper mapper)
        {
            _DbContext = context;
            _mapper = mapper;
        }

        public ReadMovieSessionDTO Create(MovieSessionDTO movieSessionDTO)
        {
            MovieSession movieSession = _mapper.Map<MovieSession>(movieSessionDTO);

            _DbContext.Add(movieSession);
            _DbContext.SaveChanges();

            ReadMovieSessionDTO readDTO = _mapper.Map<ReadMovieSessionDTO>(movieSession);

            return readDTO;
        } 

        public List<ReadMovieSessionDTO> GetAll()
        {
            List<MovieSession> movieSessionsList = _DbContext.MovieSessions.ToList();
            List<ReadMovieSessionDTO> readDTOList = _mapper.Map<List<ReadMovieSessionDTO>>(movieSessionsList);

            return readDTOList;
        }

        public ReadMovieSessionDTO GetById(int id)
        {
            MovieSession movieSession = _DbContext.MovieSessions.FirstOrDefault(ms => ms.Id == id);
        
            if(movieSession == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIESESSION);
            }

            ReadMovieSessionDTO readDTO = _mapper.Map<ReadMovieSessionDTO>(movieSession);

            return readDTO;
        }

        public ReadMovieSessionDTO Update(int id, MovieSessionDTO movieSessionDTO)
        {
            MovieSession movieSession = _DbContext.MovieSessions.FirstOrDefault(ms => ms.Id == id);

            if(movieSession == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIESESSION);
            }

            _mapper.Map(movieSessionDTO, movieSession);

            _DbContext.SaveChanges();

            ReadMovieSessionDTO readDTO = _mapper.Map<ReadMovieSessionDTO>(movieSession);

            return readDTO;
        }

        public void Delete(int id)
        {
            MovieSession movieSession = _DbContext.MovieSessions.FirstOrDefault(ms => ms.Id == id);

            if(movieSession == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIESESSION);
            }

            _DbContext.Remove(movieSession);
            _DbContext.SaveChanges();
        }
    }
}
