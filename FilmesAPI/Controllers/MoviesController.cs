using System;
using System.Net;
using AutoMapper;
using FilmesAPI.Controllers;
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
    public class MoviesController : ControllerBase
    {
        private readonly MoviesService _movieService;

        public MoviesController(MoviesService service)
        {
            _movieService = service;
        }

        [HttpPost]
        public IActionResult CreateMovie([FromBody] MovieDTO movieDTO)
        {
            try
            {
                Movie movie = _movieService.Create(movieDTO);

                return CreatedAtAction(nameof(GetMovieById), new { Id = movie.Id }, movie);
            } 
            catch(DbUpdateConcurrencyException exc) { return this.HandleException(exc); }
            catch(DbUpdateException exc) { return this.HandleException(exc); }
        }

        [HttpGet]
        public IActionResult GetAllMovies()
        {
            try
            {
                List<Movie> moviesList = _movieService.GetAll();

                return Ok(moviesList);
            }
            catch (ArgumentNullException exc) { return this.HandleException(exc); }
            catch (ArgumentException exc) { return this.HandleException(exc); }
        }

        [HttpGet("{id}")]
        public IActionResult GetMovieById(int Id)
        {
            try
            {
                ReadMovieDTO requestedMovie = _movieService.GetById(Id);
            
                return Ok(requestedMovie);
            }
            catch(ElementNotFoundException exc) { return this.HandleException(exc); }
            catch(ArgumentNullException exc) { return this.HandleException(exc); }
            catch(ArgumentException exc) { return this.HandleException(exc); }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateMovie(int Id, [FromBody] MovieDTO movie)
        {
            try
            {
                Movie updatedMovie = _movieService.Update(Id, movie);

                return Ok(updatedMovie);
            }
            catch (DbUpdateConcurrencyException exc) { return this.HandleException(exc); }
            catch (DbUpdateException exc) { return this.HandleException(exc); }
            catch (ElementNotFoundException exc) { return this.HandleException(exc); }
            catch (ArgumentNullException exc) { return this.HandleException(exc); }
            catch (ArgumentException exc) { return this.HandleException(exc); }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteMovie(int id)
        {
            try
            {
                _movieService.Delete(id);

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
