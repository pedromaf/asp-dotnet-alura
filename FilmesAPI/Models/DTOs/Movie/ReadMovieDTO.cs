using FilmesAPI.Resources;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs
{
    public class ReadMovieDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Director { get; set; }
        public string Genre { get; set; }
        public string Description { get; set; }
        public string ReleaseDate { get; set; }
        public object Sessions { get; set; }
        public int AgeRating { get; set; }
    }
}
