using FilmesAPI.Data;
using FilmesAPI.Exceptions;
using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Services
{
    public class MoviesService
    {
        private FilmesContext _DbContext;
        
        public MoviesService(FilmesContext context)
        {
            _DbContext = context;
        }

        public void Create(Movie movie)
        {
            _DbContext.Movies.Add(movie);
            _DbContext.SaveChanges();
        }

        public List<Movie> GetMoviesList()
        {
            return _DbContext.Movies.ToList();
        }

        public Movie GetMovieById(int id)
        {
            Movie movie = _DbContext.Movies.FirstOrDefault(m => m.Id == id);
            
            if(movie == null)
            {
                throw new ElementNotFoundException(ElementNotFoundException.ElementType.MOVIE);
            }

            return movie;
        }

        public Movie Update(int id, Movie newMovieData)
        {
            Movie movie = _DbContext.Movies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                throw new ElementNotFoundException(ElementNotFoundException.ElementType.MOVIE);
            }

            movie.Name = newMovieData.Name;
            movie.Director = newMovieData.Director;
            movie.Genre = newMovieData.Genre;
            movie.Description = newMovieData.Description;
            movie.ReleaseDate = newMovieData.ReleaseDate;

            _DbContext.SaveChanges();

            return movie;
        }

        public void Delete(int id)
        {
            Movie movie = _DbContext.Movies.FirstOrDefault(m => m.Id == id);

            if (movie == null)
            {
                throw new ElementNotFoundException(ElementNotFoundException.ElementType.MOVIE);
            }

            _DbContext.Remove(movie);
            _DbContext.SaveChanges();
        }
    }
}
