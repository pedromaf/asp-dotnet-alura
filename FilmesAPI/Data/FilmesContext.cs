using FilmesAPI.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace FilmesAPI.Data
{
    public class FilmesContext : DbContext
    {
        public FilmesContext(DbContextOptions<FilmesContext> opt) : base(opt)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<Address>()
                .HasOne(address => address.MovieTheater)
                .WithOne(movieTheater => movieTheater.Address)
                .HasForeignKey<MovieTheater>(movieTheater => movieTheater.AddressId);
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<MovieTheater> MovieTheaters { get; set; }
        public DbSet<Manager> Managers { get; set; }
    }
}
