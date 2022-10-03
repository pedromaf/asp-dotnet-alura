using System;
using FilmesAPI.Controllers;
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
        public IActionResult CreateMovie([FromBody] Movie movie)
        {
            movie.Id = MovieId++;
            
            MoviesList.Add(movie);

            return CreatedAtAction(nameof(GetMovieById), new { Id = movie.Id }, movie);
        }

        [HttpGet]
        public IActionResult GetAllMovies()
        {
            return Ok(MoviesList);
        }

        [HttpGet("{Id}")]
        public IActionResult GetMovieById(int Id)
        {
            Movie requestedMovie = MoviesList.FirstOrDefault(movie => movie.Id == Id);

            if(requestedMovie != null)
            {
                return Ok(requestedMovie);
            }

            return NotFound();
        }
    }
}
