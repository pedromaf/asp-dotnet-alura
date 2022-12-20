using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

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
        [Authorize(Roles = "admin")]
        public IActionResult CreateMovieSession([FromBody] MovieSessionDTO movieSessionDTO)
        {
            try
            {
                ReadMovieSessionDTO newMovieSession = _movieSessionService.Create(movieSessionDTO);

                return CreatedAtAction(nameof(GetMovieSessionById), new { newMovieSession.Id }, newMovieSession);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpGet]
        [Authorize(Roles = "admin, regular-user")]
        public IActionResult GetAllMovieSessions()
        {
            try
            {
                List<ReadMovieSessionDTO> movieSessionsList = _movieSessionService.GetAll();

                return Ok(movieSessionsList);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin, regular-user")]
        public IActionResult GetMovieSessionById(int id)
        {
            try
            {
                ReadMovieSessionDTO movieSession = _movieSessionService.GetById(id);

                return Ok(movieSession);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateMovieSession(int id, [FromBody] MovieSessionDTO movieSessionDTO)
        {
            try
            {
                ReadMovieSessionDTO updatedMovieSession = _movieSessionService.Update(id, movieSessionDTO);

                return Ok(updatedMovieSession);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult Delete(int id)
        {
            try
            {
                _movieSessionService.Delete(id);

                return NoContent();
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }
    }
}
