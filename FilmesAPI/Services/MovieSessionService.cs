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
        private FilmesContext _DbContext;
        private IMapper _mapper;

        public MovieSessionService(FilmesContext context, IMapper mapper)
        {
            _DbContext = context;
            _mapper = mapper;
        }

        public MovieSession Create(MovieSessionDTO movieSessionDTO)
        {
            MovieSession movieSession = _mapper.Map<MovieSession>(movieSessionDTO);

            _DbContext.Add(movieSession);
            _DbContext.SaveChanges();

            return movieSession;
        } 

        public List<ReadMovieSessionDTO> GetAll()
        {
            List<MovieSession> movieSessionsList = _DbContext.MovieSessions.ToList();
            List<ReadMovieSessionDTO> movieSessionsDTOList = _mapper.Map<List<ReadMovieSessionDTO>>(movieSessionsList);

            return movieSessionsDTOList;
        }

        public ReadMovieSessionDTO GetById(int id)
        {
            MovieSession movieSession = _DbContext.MovieSessions.FirstOrDefault(ms => ms.Id == id);
        
            if(movieSession == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIESESSION);
            }

            ReadMovieSessionDTO movieSessionDTO = _mapper.Map<ReadMovieSessionDTO>(movieSession);

            return movieSessionDTO;
        }

        public MovieSession Update(int id, MovieSessionDTO movieSessionDTO)
        {
            MovieSession movieSession = _DbContext.MovieSessions.FirstOrDefault(ms => ms.Id == id);

            if(movieSession == null)
            {
                throw new ElementNotFoundException(ElementType.MOVIESESSION);
            }

            _mapper.Map(movieSessionDTO, movieSession);

            _DbContext.SaveChanges();

            return movieSession;
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
