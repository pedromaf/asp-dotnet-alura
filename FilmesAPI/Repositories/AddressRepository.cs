using FilmesAPI.Data;
using FilmesAPI.Models.Entities;
using FilmesAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace FilmesAPI.Repositories
{
    public class AddressRepository : BaseRepository<Address>, IAddressRepository
    {
        public AddressRepository(FilmesContext context) : base(context)
        {

        }
    }
}
