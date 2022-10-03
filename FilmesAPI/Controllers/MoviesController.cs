using System;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        // TEMPORARY "DATABASE", IT WILL BE REPLACED BY A MySQL DATABASE SOON. IT'S JUST FOR STUDY PURPOSES.
        public static int MovieId = 0;
        public static List<Movie> MoviesList = new List<Movie>();


        [HttpPost]
        public void CreateMovie([FromBody] Movie movie)
        {
            movie.Id = MovieId++;
            
            MoviesList.Add(movie);
        }

        [HttpGet]
        public IEnumerable<Movie> GetAllMovies()
        {
            return MoviesList;
        }

        [HttpGet("{Id}")]
        public Movie GetMovieById(int Id)
        {
            Movie requestedMovie = MoviesList.FirstOrDefault(movie => movie.Id == Id);

            return requestedMovie;
        }
    }
}
