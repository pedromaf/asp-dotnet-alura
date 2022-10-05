using AutoMapper;
using FilmesAPI.Data;
using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MovieTheaterController : ControllerBase
    {
        private MovieTheaterService _movieTheaterService;
        public MovieTheaterController(MovieTheaterService service)
        {
            _movieTheaterService = service;
        }

        [HttpPost]
        public IActionResult CreateMovieTheater([FromBody] MovieTheaterDTO movieTheaterDTO)
        {
            try
            {
                MovieTheater movieTheater = _movieTheaterService.Create(movieTheaterDTO);

                return CreatedAtAction(nameof(GetMovieTheaterById), new { Id = movieTheater.Id }, movieTheater);
            }
            catch (DbUpdateConcurrencyException exc) { return this.HandleException(exc); }
            catch (DbUpdateException exc) { return this.HandleException(exc); }
            catch (ElementBeingUsedException exc) { return this.HandleException(exc); }
        }

        [HttpGet]
        public IActionResult GetAllMovieTheaters()
        {
            try
            {
                List<MovieTheater> movieTheatersList = _movieTheaterService.GetAll();

                return Ok(movieTheatersList);
            }
            catch (ArgumentNullException exc) { return this.HandleException(exc); }
            catch (ArgumentException exc) { return this.HandleException(exc); }
        }

        [HttpGet("{id}")]
        public IActionResult GetMovieTheaterById(int id)
        {
            try
            {
                ReadMovieTheaterDTO movieTheaterDTO = _movieTheaterService.GetById(id);

                return Ok(movieTheaterDTO);
            }
            catch (ElementNotFoundException exc) { return this.HandleException(exc); }
            catch (ArgumentNullException exc) { return this.HandleException(exc); }
            catch (ArgumentException exc) { return this.HandleException(exc); }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovieTheater(int id, [FromBody] MovieTheaterDTO movieTheaterDTO)
        {
            try
            {
                MovieTheater updatedMovieTheater = _movieTheaterService.Update(id, movieTheaterDTO);

                return Ok(updatedMovieTheater);
            }
            catch (DbUpdateConcurrencyException exc) { return this.HandleException(exc); }
            catch (DbUpdateException exc) { return this.HandleException(exc); }
            catch (ElementNotFoundException exc) { return this.HandleException(exc); }
            catch (ElementBeingUsedException exc) { return this.HandleException(exc); }
            catch (ArgumentNullException exc) { return this.HandleException(exc); }
            catch (ArgumentException exc) { return this.HandleException(exc); }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovieTheater(int id)
        {
            try
            {
                _movieTheaterService.Delete(id);

                return NoContent();
            }
            catch (DbUpdateConcurrencyException exc) { return this.HandleException(exc); }
            catch (DbUpdateException exc) { return this.HandleException(exc); }
            catch (ElementNotFoundException exc) { return this.HandleException(exc); }
            catch (ArgumentNullException exc) { return this.HandleException(exc); }
            catch (ArgumentException exc) { return this.HandleException(exc); }
        }
    }
}
