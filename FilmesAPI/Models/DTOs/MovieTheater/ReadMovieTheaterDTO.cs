using FilmesAPI.Models.Entities;
using FilmesAPI.Resources;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs
{
    public class ReadMovieTheaterDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public object Address { get; set; }
        public object Manager { get; set; }
        public object Sessions { get; set; }
    }
}
