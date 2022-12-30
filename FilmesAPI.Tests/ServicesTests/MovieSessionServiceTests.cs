using AutoMapper;
using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Repositories.Interfaces;
using FilmesAPI.Services;
using Moq;
using Xunit;

namespace FilmesAPI.Tests.ServicesTests
{
    public class MovieSessionServiceTests
    {
        private readonly MovieSessionService _movieSessionService;
        private readonly Mock<IMovieSessionRepository> _repositoryMock = new Mock<IMovieSessionRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        
        public MovieSessionServiceTests()
        {
            _movieSessionService = new MovieSessionService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Theory(DisplayName = "MovieSessionService.Create()")]
        [InlineData(1, 1)]
        [InlineData(2, 2)]
        [InlineData(3, 3)]
        [InlineData(4, 4)]
        [InlineData(5, 5)]
        public void CreateMovieSession(int movieId, int movieTheaterId)
        {
            //Arrange
            MovieSessionDTO movieSessionDTO = new MovieSessionDTO
            {
                MovieId = movieId,
                MovieTheaterId = movieTheaterId,
                Start = DateTime.Now,
                End = DateTime.Now,
            };

            MovieSession movieSession = new MovieSession
            {
                Id = 1,
                MovieId = movieId,
                MovieTheaterId = movieTheaterId,
                Start = DateTime.Now,
                End = DateTime.Now,
            };

            ReadMovieSessionDTO readMovieSessionDTO = new ReadMovieSessionDTO
            {
                Id = 1,
                Movie = null,
                MovieTheater = null,
                Start = DateTime.Now,
                End = DateTime.Now,
            };

            _mapperMock.Setup(mapper => mapper.Map<MovieSession>(movieSessionDTO)).Returns(movieSession);

            _repositoryMock.Setup(repository => repository.Add(movieSession));

            _mapperMock.Setup(repository => repository.Map<ReadMovieSessionDTO>(movieSession)).Returns(readMovieSessionDTO);

            //Act
            ReadMovieSessionDTO result = _movieSessionService.Create(movieSessionDTO);

            //Assert
            Assert.Equal(movieSession.Id, result.Id);
        }

        [Theory(DisplayName = "MovieSessionService.GetAll()")]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(30)]
        [InlineData(40)]
        [InlineData(50)]
        public void GetAllMovieSessions(int listSize)
        {
            //Arrange
            MovieSession movieSession;
            ReadMovieSessionDTO readMovieSessionDTO;
            List<MovieSession> movieSessionsList = new List<MovieSession>();
            List<ReadMovieSessionDTO> readMovieSessionDTOList = new List<ReadMovieSessionDTO>();

            for (int i = 0; i < listSize; i++)
            {
                movieSession = new MovieSession()
                {
                    Id = i,
                    MovieId = i,
                    MovieTheaterId = i,
                    Start = DateTime.Now,
                    End = DateTime.Now
                };

                readMovieSessionDTO = new ReadMovieSessionDTO
                {
                    Id = i,
                    Movie = null,
                    MovieTheater = null,
                    Start = DateTime.Now,
                    End = DateTime.Now
                };

                movieSessionsList.Add(movieSession);
                readMovieSessionDTOList.Add(readMovieSessionDTO);
            }

            _repositoryMock.Setup(repository => repository.GetAll())
                .Returns(movieSessionsList);

            _mapperMock.Setup(mapper => mapper.Map<List<ReadMovieSessionDTO>>(movieSessionsList))
                .Returns(readMovieSessionDTOList);

            //Act
            List<ReadMovieSessionDTO> result = _movieSessionService.GetAll();

            //Assert
            Assert.Equal(listSize, result.Count);
        }

        [Theory(DisplayName = "MovieSessionService.GetById()")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void GetMovieSessionById(int id)
        {
            //Arrange
            MovieSession movieSession = new MovieSession 
            { 
                Id = id 
            };

            ReadMovieSessionDTO readMovieSessionDTO = new ReadMovieSessionDTO
            {
                Id = id
            };

            _repositoryMock.Setup(repository => repository.GetById(id)).Returns(movieSession);

            _mapperMock.Setup(mapper => mapper.Map<ReadMovieSessionDTO>(movieSession)).Returns(readMovieSessionDTO);

            //Act
            ReadMovieSessionDTO result = _movieSessionService.GetById(id);

            //Assert
            Assert.Equal(id, result.Id);
        }

        [Fact(DisplayName = "MovieSessionService.GetById() throwing ElementNotFoundException.")]
        public void GetMovieSessionByIdWithAnInexistentId()
        {
            //Arrange
            int id = 1;

            _repositoryMock.Setup(repository => repository.GetById(id)).Returns(() => null);

            //Act
            //Assert
            Assert.Throws<ElementNotFoundException>(() =>
            {
                _movieSessionService.GetById(id);
            });
        }

        [Theory(DisplayName = "MovieSessionService.Update()")]
        [InlineData(1, 2)]
        [InlineData(2, 3)]
        [InlineData(3, 4)]
        [InlineData(4, 5)]
        [InlineData(5, 6)]
        public void UpdateMovieSession(int id, int hour)
        {
            //Arrange
            DateTime end = new DateTime(2000, 12, 2, 0, 0, 0);
            DateTime newEnd = new DateTime(2000, 12, 2, hour, 0, 0);

            MovieSession movieSession = new MovieSession
            {
                Id = id,
                End = end,
            };

            MovieSessionDTO movieSessionDTO = new MovieSessionDTO
            {
                End = newEnd
            };

            ReadMovieSessionDTO readMovieSessionDTO = new ReadMovieSessionDTO
            {
                End = newEnd
            };

            _repositoryMock.Setup(repository => repository.GetById(id))
                .Returns(movieSession);

            _mapperMock.Setup(mapper => mapper.Map(movieSessionDTO, movieSession));

            _repositoryMock.Setup(repository => repository.Update(movieSession));

            _mapperMock.Setup(mapper => mapper.Map<ReadMovieSessionDTO>(movieSession))
                .Returns(readMovieSessionDTO);
            //Act
            ReadMovieSessionDTO result = _movieSessionService.Update(id, movieSessionDTO);

            //Assert
            Assert.Equal(newEnd, result.End);
        }

        [Fact(DisplayName = "MovieSessionService.Update() throwing ElementNotFoundException.")]
        public void UpdateMovieSessionWithAnInexistentId()
        {
            //Arrange
            int id = 1;
            MovieSessionDTO movieSessionDTO = new MovieSessionDTO();

            _repositoryMock.Setup(repository => repository.GetById(id)).Returns(() => null);

            //Act
            //Assert
            Assert.Throws<ElementNotFoundException>(() =>
            {
                _movieSessionService.Update(id, movieSessionDTO);
            });
        }

        [Theory(DisplayName = "MovieSessionService.Delete()")]
        [InlineData(1)]
        public void DeleteMovie(int id)
        {
            //Arrange
            MovieSession movieSession = new MovieSession
            {
                Id = id,
            };

            _repositoryMock.Setup(repository => repository.GetById(id))
                .Returns(movieSession);

            _repositoryMock.Setup(repository => repository.Delete(movieSession));

            //Act
            _movieSessionService.Delete(id);
        }

        [Fact(DisplayName = "MovieSessionService.Delete() throwing ElementNotFoundException.")]
        public void DeleteMovieWithAnInexistentId()
        {
            //Arrange
            int id = 1;

            _repositoryMock.Setup(repository => repository.GetById(id)).Returns(() => null);

            //Act
            //Assert
            Assert.Throws<ElementNotFoundException>(() =>
            {
                _movieSessionService.Delete(id);
            });
        }
    }
}
