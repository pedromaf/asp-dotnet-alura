using FilmesAPI.Models.Entities;
using FilmesAPI.Resources;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs
{
    public class ReadMovieTheaterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Address Address { get; set; }
        public MTManager Manager { get; set; }
    }
}
