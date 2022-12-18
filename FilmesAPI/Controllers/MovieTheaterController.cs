using AutoMapper;
using FilmesAPI.Data;
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
    public class MovieTheaterController : ControllerBase
    {
        private readonly MovieTheaterService _movieTheaterService;
        public MovieTheaterController(MovieTheaterService service)
        {
            _movieTheaterService = service;
        }

        [HttpPost]
        [Authorize(Roles = "admin")]
        public IActionResult CreateMovieTheater([FromBody] MovieTheaterDTO movieTheaterDTO)
        {
            try
            {
                ReadMovieTheaterDTO newMovieTheater = _movieTheaterService.Create(movieTheaterDTO);

                return CreatedAtAction(nameof(GetMovieTheaterById), new { newMovieTheater.Id }, newMovieTheater);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpGet]
        public IActionResult GetAllMovieTheaters([FromQuery] string? movieName = null)
        {
            try
            {
                List<ReadMovieTheaterDTO> movieTheatersList = _movieTheaterService.GetAll(movieName);

                return Ok(movieTheatersList);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpGet("{id}")]
        public IActionResult GetMovieTheaterById(int id)
        {
            try
            {
                ReadMovieTheaterDTO movieTheater = _movieTheaterService.GetById(id);

                return Ok(movieTheater);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateMovieTheater(int id, [FromBody] MovieTheaterDTO movieTheaterDTO)
        {
            try
            {
                ReadMovieTheaterDTO updatedMovieTheater = _movieTheaterService.Update(id, movieTheaterDTO);

                return Ok(updatedMovieTheater);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteMovieTheater(int id)
        {
            try
            {
                _movieTheaterService.Delete(id);

                return NoContent();
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }
    }
}
