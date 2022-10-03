using System;
using System.Net;
using FilmesAPI.Controllers;
using FilmesAPI.Data;
using FilmesAPI.Exceptions;
using FilmesAPI.Models;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MoviesController : ControllerBase
    {
        private FilmesContext _DbContext;
        private MoviesService _movieService;

        public MoviesController(FilmesContext context)
        {
            _DbContext = context;
            _movieService = new MoviesService(_DbContext);
        }

        [HttpPost]
        public IActionResult CreateMovie([FromBody] Movie movie)
        {
            try
            {
                _movieService.Create(movie);

                return CreatedAtAction(nameof(GetMovieById), new { Id = movie.Id }, movie);
            } 
            catch(DbUpdateConcurrencyException exc) { return GetErrorResult(exc); }
            catch(DbUpdateException exc) { return GetErrorResult(exc); }
        }

        [HttpGet]
        public IActionResult GetAllMovies()
        {
            try
            {
                List<Movie> moviesList = _movieService.GetMoviesList();

                return Ok(moviesList);
            }
            catch (ArgumentNullException exc) { return GetErrorResult(exc); }
            catch (ArgumentException exc) { return GetErrorResult(exc); }
        }

        [HttpGet("{id}")]
        public IActionResult GetMovieById(int Id)
        {
            try
            {
                Movie requestedMovie = _movieService.GetMovieById(Id);
            
                return Ok(requestedMovie);
            }
            catch(ElementNotFoundException exc) { return GetErrorResult(exc); }
            catch(ArgumentNullException exc) { return GetErrorResult(exc); }
            catch(ArgumentException exc) { return GetErrorResult(exc); }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int Id, [FromBody] Movie movie)
        {
            try
            {
                Movie updatedMovie = _movieService.Update(Id, movie);

                return Ok(updatedMovie);
            }
            catch (DbUpdateConcurrencyException exc) { return GetErrorResult(exc); }
            catch (DbUpdateException exc) { return GetErrorResult(exc); }
            catch (ElementNotFoundException exc) { return GetErrorResult(exc); }
            catch (ArgumentNullException exc) { return GetErrorResult(exc); }
            catch (ArgumentException exc) { return GetErrorResult(exc); }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            try
            {
                _movieService.Delete(id);

                return NoContent();
            }
            catch (DbUpdateConcurrencyException exc) { return GetErrorResult(exc); }
            catch (DbUpdateException exc) { return GetErrorResult(exc); }
            catch (ElementNotFoundException exc) { return GetErrorResult(exc); }
            catch (ArgumentNullException exc) { return GetErrorResult(exc); }
            catch (ArgumentException exc) { return GetErrorResult(exc); }
        }

        private IActionResult GetErrorResult(Exception exc)
        {
            switch(exc.GetType().ToString())
            {
                case "FilmesAPI.Exceptions.ElementNotFoundException":
                    return NotFound(exc.Message);
                default:
                    return StatusCode(500, exc.Message);
            }
        } 
    }
}
