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

            builder.Entity<MovieTheater>()
                .HasOne(movieTheater => movieTheater.Manager)
                .WithMany(manager => manager.MovieTheaters)
                .HasForeignKey(movieTheater => movieTheater.ManagerId).IsRequired(false);

            builder.Entity<MovieSession>()
                .HasOne(session => session.Movie)
                .WithMany(movie => movie.Sessions)
                .HasForeignKey(session => session.MovieId);

            builder.Entity<MovieSession>()
                .HasOne(session => session.MovieTheater)
                .WithMany(movieTheater => movieTheater.Sessions)
                .HasForeignKey(session => session.MovieTheaterId);
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Address> Address { get; set; }
        public DbSet<MovieTheater> MovieTheaters { get; set; }
        public DbSet<MTManager> Managers { get; set; }
        public DbSet<MovieSession> MovieSessions { get; set; }
    }
}
