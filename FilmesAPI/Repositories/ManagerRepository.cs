using FilmesAPI.Data;
using FilmesAPI.Models.Entities;
using FilmesAPI.Repositories.Interfaces;

namespace FilmesAPI.Repositories
{
    public class ManagerRepository : BaseRepository<MTManager>, IManagerRepository
    {
        public ManagerRepository(FilmesContext context) : base(context)
        {

        }
    }
}
