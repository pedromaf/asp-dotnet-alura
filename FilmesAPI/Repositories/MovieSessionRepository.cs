using FilmesAPI.Data;
using FilmesAPI.Models.Entities;
using FilmesAPI.Repositories.Interfaces;

namespace FilmesAPI.Repositories
{
    public class MovieSessionRepository : BaseRepository<MovieSession>, IMovieSessionRepository
    {
        public MovieSessionRepository(FilmesContext context) : base(context)
        {

        }
    }
}
