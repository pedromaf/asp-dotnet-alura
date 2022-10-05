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
        private FilmesContext _DbContext;
        private MovieTheaterService _movieTheaterService;
        private IMapper _mapper;
        public MovieTheaterController(FilmesContext context, IMapper mapper)
        {
            _DbContext = context;
            _mapper = mapper;
            _movieTheaterService = new MovieTheaterService(context, mapper);
        }

        [HttpPost]
        public IActionResult CreateMovieTheater([FromBody] MovieTheaterDTO movieTheaterDTO)
        {
            try
            {
                MovieTheater movieTheater = _movieTheaterService.Create(movieTheaterDTO);

                return CreatedAtAction(nameof(GetMovieTheaterById), new { Id = movieTheater.Id }, movieTheater);
            }
            catch (DbUpdateConcurrencyException exc) { return GetErrorResult(exc); }
            catch (DbUpdateException exc) { return GetErrorResult(exc); }
        }

        [HttpGet]
        public IActionResult GetAllMovieTheaters()
        {
            try
            {
                List<MovieTheater> movieTheatersList = _movieTheaterService.GetMovieTheatersList();

                return Ok(movieTheatersList);
            }
            catch (ArgumentNullException exc) { return GetErrorResult(exc); }
            catch (ArgumentException exc) { return GetErrorResult(exc); }
        }

        [HttpGet("{id}")]
        public IActionResult GetMovieTheaterById(int id)
        {
            try
            {
                ReadMovieTheaterDTO movieTheaterDTO = _movieTheaterService.GetMovieTheaterById(id);

                return Ok(movieTheaterDTO);
            }
            catch (ElementNotFoundException exc) { return GetErrorResult(exc); }
            catch (ArgumentNullException exc) { return GetErrorResult(exc); }
            catch (ArgumentException exc) { return GetErrorResult(exc); }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovieTheater(int id, [FromBody] MovieTheaterDTO movieTheaterDTO)
        {
            try
            {
                MovieTheater updatedMovieTheater = _movieTheaterService.Update(id, movieTheaterDTO);

                return Ok(updatedMovieTheater);
            }
            catch (DbUpdateConcurrencyException exc) { return GetErrorResult(exc); }
            catch (DbUpdateException exc) { return GetErrorResult(exc); }
            catch (ElementNotFoundException exc) { return GetErrorResult(exc); }
            catch (ArgumentNullException exc) { return GetErrorResult(exc); }
            catch (ArgumentException exc) { return GetErrorResult(exc); }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovieTheater(int id)
        {
            try
            {
                _movieTheaterService.Delete(id);

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
            switch (exc.GetType().ToString())
            {
                case "FilmesAPI.Exceptions.ElementNotFoundException":
                    return NotFound(exc.Message);
                default:
                    return StatusCode(500, exc.Message);
            }
        }
    }
}
