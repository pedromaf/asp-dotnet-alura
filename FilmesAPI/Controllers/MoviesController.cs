using System;
using System.Net;
using AutoMapper;
using FilmesAPI.Controllers;
using FilmesAPI.Data;
using FilmesAPI.Exceptions;
using FilmesAPI.Models.DTOs;
using FilmesAPI.Models.Entities;
using FilmesAPI.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
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
        [Authorize(Roles = "admin")]
        public IActionResult CreateMovie([FromBody] MovieDTO movieDTO)
        {
            try
            {
                ReadMovieDTO newMovie = _movieService.Create(movieDTO);

                return CreatedAtAction(nameof(GetMovieById), new { newMovie.Id }, newMovie);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpGet]
        [Authorize(Roles = "admin, regular-user", Policy = "min-age")]
        public IActionResult GetAllMovies([FromQuery] int? ageRating = null)
        {
            try
            {
                List<ReadMovieDTO> moviesList = _movieService.GetAll(ageRating);

                return Ok(moviesList);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpGet("{id}")]
        [Authorize(Roles = "admin, regular-user", Policy = "min-age")]
        public IActionResult GetMovieById(int Id)
        {
            try
            {
                ReadMovieDTO movie = _movieService.GetById(Id);
            
                return Ok(movie);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult UpdateMovie(int Id, [FromBody] MovieDTO movie)
        {
            try
            {
                ReadMovieDTO updatedMovie = _movieService.Update(Id, movie);

                return Ok(updatedMovie);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "admin")]
        public IActionResult DeleteMovie(int id)
        {
            try
            {
                _movieService.Delete(id);

                return NoContent();
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }
    }
}
