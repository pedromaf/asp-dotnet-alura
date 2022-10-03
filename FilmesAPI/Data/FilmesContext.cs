using FilmesAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    public class FilmesContext : DbContext
    {
        public FilmesContext(DbContextOptions<FilmesContext> opt) : base(opt)
        {

        }

        public DbSet<Movie> Movies { get; set; }
    }
}
