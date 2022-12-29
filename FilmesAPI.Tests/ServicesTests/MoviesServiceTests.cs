using AutoMapper;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Repositories.Interfaces;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Routing;
using Moq;
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

        [Theory]
        [InlineData("Filme 1", "Diretor 1", "Genero 1", "Descricao 1", "Data de lançamento 1", 10)]
        [InlineData("Filme 2", "Diretor 2", "Genero 2", "Descricao 2", "Data de lançamento 2", 12)]
        [InlineData("Filme 3", "Diretor 3", "Genero 3", "Descricao 3", "Data de lançamento 3", 4)]
        [InlineData("Filme 4", "Diretor 4", "Genero 4", "Descricao 4", "Data de lançamento 4", 20)]
        [InlineData("Filme 5", "Diretor 5", "Genero 5", "Descricao 5", "Data de lançamento 5", 18)]
        public void Create(string name, string director, string genre, string description, string releaseDate, int ageRating)
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

            _mapperMock.Setup(mapper => mapper.Map<Movie>(movieDTO)).Returns(movie);
            _mapperMock.Setup(mapper => mapper.Map<ReadMovieDTO>(movie)).Returns(readMovieDTO);

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
    }
}
