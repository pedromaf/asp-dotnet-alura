using AutoMapper;
using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Repositories.Interfaces;
using FilmesAPI.Services;
using Microsoft.EntityFrameworkCore.Query.Internal;
using Moq;
using Xunit;

namespace FilmesAPI.Tests.ServicesTests
{
    public class MovieTheaterServiceTests
    {
        private readonly MovieTheaterService _movieTheaterService;
        private readonly Mock<IMovieTheaterRepository> _repositoryMock = new Mock<IMovieTheaterRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        public MovieTheaterServiceTests()
        {
            _movieTheaterService = new MovieTheaterService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Theory(DisplayName = "MovieTheaterService.Create()")]
        [InlineData("Cinema 1")]
        [InlineData("Cinema 2")]
        [InlineData("Cinema 3")]
        [InlineData("Cinema 4")]
        [InlineData("Cinema 5")]
        public void CreateMovieTheater(string name)
        {
            //Arrange
            MovieTheaterDTO movieTheaterDTO = new MovieTheaterDTO
            {
                Name = name,
                AddressId = 1
            };

            MovieTheater movieTheater = new MovieTheater
            {
                Name = name,
                AddressId = 1
            };

            ReadMovieTheaterDTO readMovieTheaterDTO = new ReadMovieTheaterDTO
            {
                Name = name,
            };

            _repositoryMock.Setup(repository => repository.VerifyIfAddressIsBeingUsed(movieTheaterDTO.AddressId))
                .Returns(false);

            _mapperMock.Setup(mapper => mapper.Map<MovieTheater>(movieTheaterDTO))
                .Returns(movieTheater);

            _repositoryMock.Setup(repository => repository.Add(movieTheater));

            _mapperMock.Setup(mapper => mapper.Map<ReadMovieTheaterDTO>(movieTheater))
                .Returns(readMovieTheaterDTO);

            //Act
            ReadMovieTheaterDTO result = _movieTheaterService.Create(movieTheaterDTO);

            //Assert
            Assert.Equal(name, result.Name);
        }

        [Fact(DisplayName = "MovieTheaterService.Create() throwing ElementBeingUsedException.")]
        public void CreateMovieTheaterWithAnInexistentAddressId()
        {
            //Arrange
            int addressId = 1;
            
            MovieTheaterDTO movieTheaterDTO = new MovieTheaterDTO
            {
                AddressId = addressId
            };

            _repositoryMock.Setup(repository => repository.VerifyIfAddressIsBeingUsed(addressId)).Returns(true);

            //Act
            //Assert
            Assert.Throws<ElementBeingUsedException>(() =>
            {
                ReadMovieTheaterDTO result = _movieTheaterService.Create(movieTheaterDTO);
            });
        }

        [Theory(DisplayName = "MovieTheaterService.GetAll() without movie name filter.")]
        [InlineData(0)]
        [InlineData(10)]
        [InlineData(20)]
        [InlineData(30)]
        [InlineData(40)]
        public void GetAllMovieTheatersWithoutMovieNameFilter(int listSize)
        {
            //Arrange
            MovieTheater movieTheater;
            ReadMovieTheaterDTO readMovieTheaterDTO;
            List<MovieTheater> movieTheaterList = new List<MovieTheater>();
            List<ReadMovieTheaterDTO> readMovieTheaterDTOList = new List<ReadMovieTheaterDTO>();

            for (int i = 0; i < listSize; i++)
            {
                movieTheater = new MovieTheater
                {
                    Name = $"Cinema {i}"
                };

                readMovieTheaterDTO = new ReadMovieTheaterDTO
                {
                    Name = $"Cinema {i}"
                };

                movieTheaterList.Add(movieTheater);
                readMovieTheaterDTOList.Add(readMovieTheaterDTO);
            }

            _repositoryMock.Setup(repository => repository.GetAll()).Returns(movieTheaterList);

            _mapperMock.Setup(mapper => mapper.Map<List<ReadMovieTheaterDTO>>(movieTheaterList)).Returns(readMovieTheaterDTOList);

            //Act
            List<ReadMovieTheaterDTO> result = _movieTheaterService.GetAll(null);

            //Assert
            Assert.Equal(listSize, result.Count);
        }

        [Theory(DisplayName = "MovieTheaterService.GetAll() with movie name filter")]
        [InlineData(0, "Filme teste 1")]
        [InlineData(10, "Filme teste 2")]
        [InlineData(20, "Filme teste 3")]
        [InlineData(30, "Filme teste 4")]
        [InlineData(40, "Filme teste 5")]
        public void GetAllMovieTheatersWithMovieNameFilter(int listSize, string movieName)
        {
            //Arrange
            MovieTheater movieTheater;
            ReadMovieTheaterDTO readMovieTheaterDTO;
            MovieSession movieSession;
            List<MovieTheater> movieTheaterList = new List<MovieTheater>();
            List<MovieTheater> movieTheaterListFiltered = new List<MovieTheater>();
            List<ReadMovieTheaterDTO> readMovieTheaterDTOList = new List<ReadMovieTheaterDTO>();
            List<MovieSession> sessionList = new List<MovieSession>();
            List<MovieSession> emptySessionList = new List<MovieSession>();

            for (int i = 0; i < 10; i++)
            {
                movieSession = new MovieSession
                {
                    Movie = new Movie()
                    {
                        Name = $"Filme teste {i}"
                    }
                };

                sessionList.Add(movieSession);
            }

            for (int i = 0; i < listSize; i++)
            {
                if (i < listSize/2)
                {
                    movieTheater = new MovieTheater
                    {
                        Name = $"Cinema {i}",
                        Sessions = sessionList
                    };

                    readMovieTheaterDTO = new ReadMovieTheaterDTO
                    {
                        Name = $"Cinema {i}",
                        Sessions = sessionList
                    };
                    
                    readMovieTheaterDTOList.Add(readMovieTheaterDTO);
                    movieTheaterListFiltered.Add(movieTheater);
                } 
                else
                {
                    movieTheater = new MovieTheater
                    {
                        Name = $"Cinema {i}",
                        Sessions = emptySessionList
                    };
                }

                movieTheaterList.Add(movieTheater);
            }

            _repositoryMock.Setup(repository => repository.GetAll()).Returns(movieTheaterList);

            _mapperMock.Setup(mapper => mapper.Map<List<ReadMovieTheaterDTO>>(movieTheaterListFiltered)).Returns(readMovieTheaterDTOList);

            //Act
            List<ReadMovieTheaterDTO> result = _movieTheaterService.GetAll(movieName);

            //Assert
            Assert.Equal((listSize/2), result.Count);
        }

        [Theory(DisplayName = "MovieTheaterService.GetById()")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void GetMovieTheaterById(int id)
        {
            //Arrange
            MovieTheater movieTheater = new MovieTheater
            {
                Id = id
            };

            ReadMovieTheaterDTO readMovieTheaterDTO = new ReadMovieTheaterDTO
            {
                Id = id
            };

            _repositoryMock.Setup(repository => repository.GetById(id)).Returns(movieTheater);

            _mapperMock.Setup(mapper => mapper.Map<ReadMovieTheaterDTO>(movieTheater)).Returns(readMovieTheaterDTO);

            //Act
            ReadMovieTheaterDTO result = _movieTheaterService.GetById(id);

            //Assert
            Assert.Equal(id, result.Id);
        }

        [Fact(DisplayName = "MovieTheaterService.GetById() throwing ElementNotFoundException.")]
        public void GetMovieTheaterByIdWithAnInexistentId()
        {
            //Arrange
            int id = 1;

            _repositoryMock.Setup(repository => repository.GetById(id)).Returns(() => null);

            //Act
            //Assert
            Assert.Throws<ElementNotFoundException>(() =>
            {
                ReadMovieTheaterDTO result = _movieTheaterService.GetById(id);
            });
        }

        [Theory(DisplayName = "MovieTheaterService.Update()")]
        [InlineData(1, "Cinema teste 1", 1)]
        [InlineData(2, "Cinema teste 2", 2)]
        [InlineData(3, "Cinema teste 3", 3)]
        [InlineData(4, "Cinema teste 4", 4)]
        [InlineData(5, "Cinema teste 5", 5)]
        public void UpdateMovieTheater(int id, string newName, int newAddressId)
        {
            //Arrange
            MovieTheaterDTO movieTheaterDTO = new MovieTheaterDTO
            {
                Name = newName,
                AddressId = newAddressId
            };

            MovieTheater movieTheater = new MovieTheater()
            {
                Id = id,
                Name = "Cinema",
                AddressId = 0
            };

            ReadMovieTheaterDTO readMovieTheaterDTO = new ReadMovieTheaterDTO
            {
                Id = id,
                Name = newName
            };

            _repositoryMock.Setup(repository => repository.GetById(id))
                .Returns(movieTheater);
            
            _repositoryMock.Setup(repository => repository.VerifyIfAddressIsBeingUsed(movieTheaterDTO.AddressId))
                .Returns(false);

            _mapperMock.Setup(mapper => mapper.Map(movieTheaterDTO, movieTheater));

            _repositoryMock.Setup(repository => repository.Update(movieTheater));

            _mapperMock.Setup(mapper => mapper.Map<ReadMovieTheaterDTO>(movieTheater))
                .Returns(readMovieTheaterDTO);

            //Act
            ReadMovieTheaterDTO result = _movieTheaterService.Update(id, movieTheaterDTO);

            //Assert
            Assert.Equal(newName, result.Name);
        }

        [Fact(DisplayName = "MovieTheaterService.Update() throwing ElementNotFoundException.")]
        public void UpdateMovieTheaterWithAnInexistentMovieTheaterId()
        {
            //Arrange
            int id = 1;
            MovieTheaterDTO movieTheaterDTO = new MovieTheaterDTO();

            _repositoryMock.Setup(repository => repository.GetById(id))
                .Returns(() => null);

            //Act
            //Arrange
            Assert.Throws<ElementNotFoundException>(() =>
            {
                ReadMovieTheaterDTO result = _movieTheaterService.Update(id, movieTheaterDTO);
            });
        }

        [Fact(DisplayName = "MovieTheaterService.Update() throwing ElementBeingUsedException.")]
        public void UpdateMovieTheaterWithAnInexistentAddressId()
        {
            //Arrange
            int id = 1;
            int addressId = 1;

            MovieTheaterDTO movieTheaterDTO = new MovieTheaterDTO()
            {
                AddressId = addressId
            };

            MovieTheater movieTheater = new MovieTheater
            {
                Id = id,
                AddressId = 2
            };

            _repositoryMock.Setup(repository => repository.GetById(id))
                .Returns(() => movieTheater);

            _repositoryMock.Setup(repository => repository.VerifyIfAddressIsBeingUsed(movieTheaterDTO.AddressId)).Returns(true);

            //Act
            //Arrange
            Assert.Throws<ElementBeingUsedException>(() =>
            {
                ReadMovieTheaterDTO result = _movieTheaterService.Update(id, movieTheaterDTO);
            });
        }

        [Theory(DisplayName = "MovieTheaterService.Delete()")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void DeleteMovieTheater(int id)
        {
            //Arrange
            MovieTheater movieTheater = new MovieTheater
            {
                Id = id
            };

            _repositoryMock.Setup(repository => repository.GetById(id))
                .Returns(movieTheater);

            _repositoryMock.Setup(repository => repository.Delete(movieTheater));

            //Act
            _movieTheaterService.Delete(id);
        }

        [Fact(DisplayName = "MovieTheaterService.Delete() throwing ElementNotFoundException.")]
        public void DeleteMovieTheaterWithAnInexistentId()
        {
            //Arrange
            int id = 1;

            _repositoryMock.Setup(repository => repository.GetById(id))
                .Returns(() => null);

            //Act
            //Assert
            Assert.Throws<ElementNotFoundException>(() =>
            {
                _movieTheaterService.Delete(id);
            });
        }
    }
}
