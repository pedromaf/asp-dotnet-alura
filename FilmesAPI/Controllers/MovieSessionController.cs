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
    public class MovieSessionController : ControllerBase
    {
        private readonly MovieSessionService _movieSessionService;

        public MovieSessionController(MovieSessionService service)
        {
            _movieSessionService = service;
        }

        [HttpPost]
        public IActionResult CreateMovieSession([FromBody] MovieSessionDTO movieSessionDTO)
        {
            try
            {
                ReadMovieSessionDTO newMovieSession = _movieSessionService.Create(movieSessionDTO);

                return CreatedAtAction(nameof(GetMovieSessionById), new { newMovieSession.Id }, newMovieSession);
            }
            catch (DbUpdateConcurrencyException exc) { return this.HandleException(exc); }
            catch (DbUpdateException exc) { return this.HandleException(exc); }
        }

        [HttpGet]
        public IActionResult GetAllMovieSessions()
        {
            try
            {
                List<ReadMovieSessionDTO> movieSessionsList = _movieSessionService.GetAll();

                return Ok(movieSessionsList);
            }
            catch (ArgumentNullException exc) { return this.HandleException(exc); }
            catch (ArgumentException exc) { return this.HandleException(exc); }
        }

        [HttpGet("{id}")]
        public IActionResult GetMovieSessionById(int id)
        {
            try
            {
                ReadMovieSessionDTO movieSession = _movieSessionService.GetById(id);

                return Ok(movieSession);
            }
            catch (ElementNotFoundException exc) { return this.HandleException(exc); }
            catch (ArgumentNullException exc) { return this.HandleException(exc); }
            catch (ArgumentException exc) { return this.HandleException(exc); }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovieSession(int id, [FromBody] MovieSessionDTO movieSessionDTO)
        {
            try
            {
                ReadMovieSessionDTO updatedMovieSession = _movieSessionService.Update(id, movieSessionDTO);

                return Ok(updatedMovieSession);
            }
            catch (DbUpdateConcurrencyException exc) { return this.HandleException(exc); }
            catch (DbUpdateException exc) { return this.HandleException(exc); }
            catch (ElementNotFoundException exc) { return this.HandleException(exc); }
            catch (ArgumentNullException exc) { return this.HandleException(exc); }
            catch (ArgumentException exc) { return this.HandleException(exc); }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _movieSessionService.Delete(id);

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
