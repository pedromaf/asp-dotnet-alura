using FilmesAPI.Data;
using FilmesAPI.Models.Entities;
using FilmesAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace FilmesAPI.Repositories
{
    public class MovieTheaterRepository : BaseRepository<MovieTheater>, IMovieTheaterRepository
    {
        private readonly FilmesContext _context;
        public MovieTheaterRepository(FilmesContext context) : base(context)
        {
            _context = context;
        }

        public bool VerifyIfAddressIsBeingUsed(int addressId)
        {
            return _context.MovieTheaters.Any(movieTheater => movieTheater.AddressId == addressId);
        }
    }
}
