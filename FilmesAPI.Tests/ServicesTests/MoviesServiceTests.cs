using AutoMapper;
using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Repositories.Interfaces;
using FilmesAPI.Services;
using Moq;
using System.IO;
using System.Xml.Linq;
using Xunit;

namespace FilmesAPI.Tests.ServicesTests
{
    public class MoviesServiceTests
    {
        private readonly MoviesService _movieService;
        private readonly Mock<IMovieRepository> _repositoryMock = new Mock<IMovieRepository>();
        private readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();
        public MoviesServiceTests()
        {
            _movieService = new MoviesService(_repositoryMock.Object, _mapperMock.Object);
        }

        [Theory(DisplayName = "MovieService.Create().")]
        [InlineData("Filme 1", "Diretor 1", "Genero 1", "Descricao 1", "Data de lançamento 1", 10)]
        [InlineData("Filme 2", "Diretor 2", "Genero 2", "Descricao 2", "Data de lançamento 2", 12)]
        [InlineData("Filme 3", "Diretor 3", "Genero 3", "Descricao 3", "Data de lançamento 3", 4)]
        [InlineData("Filme 4", "Diretor 4", "Genero 4", "Descricao 4", "Data de lançamento 4", 20)]
        [InlineData("Filme 5", "Diretor 5", "Genero 5", "Descricao 5", "Data de lançamento 5", 18)]
        public void CreateMovie(string name, string director, string genre, string description, string releaseDate, int ageRating)
        {
            //Arrange
            MovieDTO movieDTO = new MovieDTO
            {
                Name = name,
                Director = director,
                Genre = genre,
                Description = description,
                ReleaseDate = releaseDate,
                AgeRating = ageRating
            };

            Movie movie = new Movie
            {
                Name = name,
                Director = director,
                Genre = genre,
                Description = description,
                ReleaseDate = releaseDate,
                AgeRating = ageRating
            };

            ReadMovieDTO readMovieDTO = new ReadMovieDTO
            {
                Name = name,
                Director = director,
                Genre = genre,
                Description = description,
                ReleaseDate = releaseDate,
                AgeRating = ageRating
            };

            _mapperMock.Setup(mapper => mapper.Map<Movie>(movieDTO))
                .Returns(movie);
            _mapperMock.Setup(mapper => mapper.Map<ReadMovieDTO>(movie))
                .Returns(readMovieDTO);

            _repositoryMock.Setup(repository => repository.Add(movie));
            
            //Act
            ReadMovieDTO result = _movieService.Create(movieDTO);

            //Assert
            Assert.Equal(readMovieDTO.Name, result.Name);
            Assert.Equal(readMovieDTO.Director, result.Director);
            Assert.Equal(readMovieDTO.Genre, result.Genre);
            Assert.Equal(readMovieDTO.Description, result.Description);
            Assert.Equal(readMovieDTO.ReleaseDate, result.ReleaseDate);
            Assert.Equal(readMovieDTO.AgeRating, result.AgeRating);
        }

        [Theory(DisplayName = "MovieService.GetAll() without age filtering.")]
        [InlineData(0)]
        [InlineData(5)]
        [InlineData(10)]
        [InlineData(14)]
        [InlineData(23)]
        [InlineData(30)]
        [InlineData(50)]
        public void GetAllMoviesWithoutAgeRatingFilter(int listSize)
        {
            //Arrange
            Movie movie;
            ReadMovieDTO readMovieDTO;
            List<Movie> movieList = new List<Movie>();
            List<ReadMovieDTO> readMovieDTOList = new List<ReadMovieDTO>();
            
            for(int i = 0; i < listSize; i++)
            {
                movie = new Movie()
                {
                    Name = $"Movie {i}",
                    Director = $"Director {i}",
                    Genre = $"Genero {i}",
                    Description = $"Descricao {i}",
                    ReleaseDate = $"Data de lançamento {i}",
                    AgeRating = i
                };

                readMovieDTO = new ReadMovieDTO
                {
                    Name = $"Movie {i}",
                    Director = $"Director {i}",
                    Genre = $"Genero {i}",
                    Description = $"Descricao {i}",
                    ReleaseDate = $"Data de lançamento {i}",
                    AgeRating = i
                };

                movieList.Add(movie);
                readMovieDTOList.Add(readMovieDTO);
            } 

            _repositoryMock.Setup(repository => repository.GetAll())
                .Returns(movieList);

            _mapperMock.Setup(mapper => mapper.Map<List<ReadMovieDTO>>(movieList))
                .Returns(readMovieDTOList);

            //Act
            List<ReadMovieDTO> result = _movieService.GetAll(null);

            //Assert
            Assert.Equal(listSize, result.Count);
        }

        [Theory(DisplayName = "MovieService.GetAll() with age filtering.")]
        [InlineData(0, 10)]
        [InlineData(5, 10)]
        [InlineData(10, 18)]
        [InlineData(14, 12)]
        [InlineData(23, 8)]
        [InlineData(30, 4)]
        [InlineData(50, 16)]
        public void GetAllMoviesWithAgeRatingFilter(int listSize, int ageRating)
        {
            //Arrange
            Movie movie;
            ReadMovieDTO readMovieDTO;
            List<Movie> movieList = new List<Movie>();
            List<ReadMovieDTO> readMovieDTOList = new List<ReadMovieDTO>();
            int expectedListSize;

            if (listSize < ageRating)
            {
                expectedListSize = listSize;
            }
            else
            {
                expectedListSize = ageRating + 1;
            }

            for (int i = 0; i < listSize; i++)
            {
                movie = new Movie()
                {
                    Name = $"Movie {i}",
                    Director = $"Director {i}",
                    Genre = $"Genero {i}",
                    Description = $"Descricao {i}",
                    ReleaseDate = $"Data de lançamento {i}",
                    AgeRating = i
                };

                if (i <= ageRating)
                {
                    readMovieDTO = new ReadMovieDTO
                    {
                        Name = $"Movie {i}",
                        Director = $"Director {i}",
                        Genre = $"Genero {i}",
                        Description = $"Descricao {i}",
                        ReleaseDate = $"Data de lançamento {i}",
                        AgeRating = i
                    };
                    
                    readMovieDTOList.Add(readMovieDTO);
                }

                movieList.Add(movie);
            }

            _repositoryMock.Setup(repository => repository.GetAll())
                .Returns(movieList);

            _mapperMock.Setup(mapper => mapper.Map<List<ReadMovieDTO>>(movieList))
                .Returns(readMovieDTOList);

            //Act
            List<ReadMovieDTO> result = _movieService.GetAll(ageRating);

            //Assert
            Assert.Equal(expectedListSize, result.Count);
        }

        [Theory(DisplayName = "MovieService.GetById()")]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(4)]
        [InlineData(5)]
        public void GetMovieById(int id)
        {
            //Arrange
            Movie movie = new Movie
            {
                Id = id,
                Name = "Filme teste",
                Director = "Diretor teste",
                Genre = "Genero teste",
                Description = "Descricao teste",
                ReleaseDate = "Data de lancamento teste",
                AgeRating = 18
            };

            ReadMovieDTO readMovieDTO = new ReadMovieDTO
            {
                Id = id,
                Name = "Filme teste",
                Director = "Diretor teste",
                Genre = "Genero teste",
                Description = "Descricao teste",
                ReleaseDate = "Data de lancamento teste",
                AgeRating = 18
            };

            _repositoryMock.Setup(repository => repository.GetById(id))
                .Returns(movie);

            _mapperMock.Setup(mapper => mapper.Map<ReadMovieDTO>(movie))
                .Returns(readMovieDTO);
            
            //Act
            ReadMovieDTO result = _movieService.GetById(id);

            //Assert
            Assert.Equal(id, result.Id);
        }

        [Fact(DisplayName = "MovieService.GetById() throwing ElementNotFoundException.")]
        public void GetMovieByIdWithAnInexistentId()
        {
            //Arrange
            int id = 1;

            _repositoryMock.Setup(repository => repository.GetById(id))
                .Returns(() => null);

            //Act
            //Assert
            Assert.Throws<ElementNotFoundException>(() =>
            {
                ReadMovieDTO result = _movieService.GetById(id);
            });
        }

        [Theory(DisplayName = "MovieService.Update() updating ageRating.")]
        [InlineData(1, 12)]
        [InlineData(2, 10)]
        [InlineData(3, 5)]
        [InlineData(4, 17)]
        [InlineData(5, 14)]
        public void UpdateMovie(int id, int newAgeRating)
        {
            //Arrange
            Movie movie = new Movie
            {
                Name = "Filme teste",
                Director = "Diretor teste",
                Genre = "Genero teste",
                Description = "Descricao teste",
                ReleaseDate = "Data de lancamento teste",
                AgeRating = 18
            };

            MovieDTO movieDTO = new MovieDTO
            {
                Name = "Filme teste",
                Director = "Diretor teste",
                Genre = "Genero teste",
                Description = "Descricao teste",
                ReleaseDate = "Data de lancamento teste",
                AgeRating = newAgeRating
            };

            ReadMovieDTO readMovieDTO = new ReadMovieDTO
            {
                Name = "Filme teste",
                Director = "Diretor teste",
                Genre = "Genero teste",
                Description = "Descricao teste",
                ReleaseDate = "Data de lancamento teste",
                AgeRating = newAgeRating
            };

            _repositoryMock.Setup(repository => repository.GetById(id))
                .Returns(movie);

            _mapperMock.Setup(mapper => mapper.Map(movieDTO, movie));

            _repositoryMock.Setup(repository => repository.Update(movie));

            _mapperMock.Setup(mapper => mapper.Map<ReadMovieDTO>(movie))
                .Returns(readMovieDTO);

            //Act
            ReadMovieDTO result = _movieService.Update(id, movieDTO);

            //Assert
            Assert.Equal(newAgeRating, result.AgeRating);
        }

        [Fact(DisplayName = "MovieService.Update() throwing ElementNotFoundException.")]
        public void UpdateMovieWithAnInexistentId()
        {
            //Arrange
            int id = 1;
            MovieDTO movieDTO = new MovieDTO
            {
                Name = "Filme teste",
                Director = "Diretor teste",
                Genre = "Genero teste",
                Description = "Descricao teste",
                ReleaseDate = "Data de lancamento teste",
                AgeRating = 18
            };

            _repositoryMock.Setup(repository => repository.GetById(id))
                .Returns(() => null);

            //Act
            //Assert
            Assert.Throws<ElementNotFoundException>(() =>
            {
                ReadMovieDTO result = _movieService.Update(id, movieDTO);
            });
        }

        [Theory(DisplayName = "MovieService.Delete()")]
        [InlineData(1)]
        public void DeleteMovie(int id)
        {
            //Arrange
            Movie movie = new Movie
            {
                Id = id,
                Name = "Filme teste",
                Director = "Diretor teste",
                Genre = "Genero teste",
                Description = "Descricao teste",
                ReleaseDate = "Data de lancamento teste",
                AgeRating = 18
            };

            _repositoryMock.Setup(repository => repository.GetById(id))
                .Returns(movie);

            _repositoryMock.Setup(repository => repository.Delete(movie));

            //Act
            _movieService.Delete(id);
        }

        [Fact(DisplayName = "MovieService.Delete() throwing ElementNotFoundException.")]
        public void DeleteMovieWithAnInexistentId()
        {
            //Arrange
            int id = 1;

            _repositoryMock.Setup(repository => repository.GetById(id)).Returns(() => null);

            //Act
            //Assert
            Assert.Throws<ElementNotFoundException>(() =>
            {
                _movieService.Delete(id);
            });
        }
    }
}
