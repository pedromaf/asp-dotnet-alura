using FilmesAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    public class FilmesContext : DbContext
    {
        public FilmesContext(DbContextOptions<FilmesContext> opt) : base(opt)
        {

        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Address> Address { get; set; }
    }
}
