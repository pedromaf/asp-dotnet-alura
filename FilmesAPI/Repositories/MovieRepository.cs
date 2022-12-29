using FilmesAPI.Data;
using FilmesAPI.Models.Entities;
using FilmesAPI.Repositories.Interfaces;

namespace FilmesAPI.Repositories
{
    public class MovieRepository : BaseRepository<Movie>, IMovieRepository
    {
        public MovieRepository(FilmesContext context) : base(context)
        {

        }
    }
}
