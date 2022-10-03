using System;
using FilmesAPI.Models;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    [ApiController]
    public class MoviesController : ControllerBase
    {
        [HttpPost]
        [Route("/movies")]
        public void CreateMovie([FromBody] Movie movie)
        {
            Console.WriteLine(movie.Description);
        }
    }
}
