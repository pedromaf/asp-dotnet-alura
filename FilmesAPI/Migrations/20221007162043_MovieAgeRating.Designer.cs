// <auto-generated />
using System;
using FilmesAPI.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace FilmesAPI.Migrations
{
    [DbContext(typeof(FilmesContext))]
    [Migration("20221007162043_MovieAgeRating")]
    partial class MovieAgeRating
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("FilmesAPI.Models.Entities.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<string>("District")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.Property<int>("PostalCode")
                        .HasColumnType("int");

                    b.Property<string>("Street")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("FilmesAPI.Models.Entities.Movie", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AgeRating")
                        .HasColumnType("int");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasMaxLength(500)
                        .HasColumnType("varchar(500)");

                    b.Property<string>("Director")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Genre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)");

                    b.Property<string>("ReleaseDate")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Movies");
                });

            modelBuilder.Entity("FilmesAPI.Models.Entities.MovieSession", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MovieId")
                        .HasColumnType("int");

                    b.Property<int>("MovieTheaterId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime(6)");

                    b.HasKey("Id");

                    b.HasIndex("MovieId");

                    b.HasIndex("MovieTheaterId");

                    b.ToTable("MovieSessions");
                });

            modelBuilder.Entity("FilmesAPI.Models.Entities.MovieTheater", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("AddressId")
                        .HasColumnType("int");

                    b.Property<int>("ManagerId")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.HasIndex("AddressId")
                        .IsUnique();

                    b.HasIndex("ManagerId");

                    b.ToTable("MovieTheaters");
                });

            modelBuilder.Entity("FilmesAPI.Models.Entities.MTManager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)");

                    b.HasKey("Id");

                    b.ToTable("Managers");
                });

            modelBuilder.Entity("FilmesAPI.Models.Entities.MovieSession", b =>
                {
                    b.HasOne("FilmesAPI.Models.Entities.Movie", "Movie")
                        .WithMany("Sessions")
                        .HasForeignKey("MovieId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmesAPI.Models.Entities.MovieTheater", "MovieTheater")
                        .WithMany("Sessions")
                        .HasForeignKey("MovieTheaterId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Movie");

                    b.Navigation("MovieTheater");
                });

            modelBuilder.Entity("FilmesAPI.Models.Entities.MovieTheater", b =>
                {
                    b.HasOne("FilmesAPI.Models.Entities.Address", "Address")
                        .WithOne("MovieTheater")
                        .HasForeignKey("FilmesAPI.Models.Entities.MovieTheater", "AddressId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("FilmesAPI.Models.Entities.MTManager", "Manager")
                        .WithMany("MovieTheaters")
                        .HasForeignKey("ManagerId");

                    b.Navigation("Address");

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("FilmesAPI.Models.Entities.Address", b =>
                {
                    b.Navigation("MovieTheater")
                        .IsRequired();
                });

            modelBuilder.Entity("FilmesAPI.Models.Entities.Movie", b =>
                {
                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("FilmesAPI.Models.Entities.MovieTheater", b =>
                {
                    b.Navigation("Sessions");
                });

            modelBuilder.Entity("FilmesAPI.Models.Entities.MTManager", b =>
                {
                    b.Navigation("MovieTheaters");
                });
#pragma warning restore 612, 618
        }
    }
}
