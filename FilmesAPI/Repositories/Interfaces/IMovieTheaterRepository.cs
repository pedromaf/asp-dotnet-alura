using FilmesAPI.Models.Entities;

namespace FilmesAPI.Repositories.Interfaces
{
    public interface IMovieTheaterRepository : IRepository<MovieTheater>
    {
        public bool VerifyIfAddressIsBeingUsed(int addressId);
    }
}
