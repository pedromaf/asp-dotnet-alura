using FilmesAPI.Resources;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs
{
    public class MovieDTO
    {
        [Required(ErrorMessage = Messages.MOVIE_NAME_REQUIRED)]
        [StringLength(100, ErrorMessage = Messages.MOVIE_NAME_TOO_LONG)]
        public string Name { get; set; }

        [Required(ErrorMessage = Messages.MOVIE_DIRECTOR_REQUIRED)]
        [StringLength(100, ErrorMessage = Messages.MOVIE_DIRECTOR_TOO_LONG)]
        public string Director { get; set; }

        [Required(ErrorMessage = Messages.MOVIE_GENRE_REQUIRED)]
        [StringLength(100, ErrorMessage = Messages.MOVIE_GENRE_TOO_LONG)]
        public string Genre { get; set; }

        [Required(ErrorMessage = Messages.MOVIE_DESCRIPTION_REQUIRED)]
        [MinLength(50, ErrorMessage = Messages.MOVIE_DESCRIPTION_TOO_SMALL)]
        [MaxLength(500, ErrorMessage = Messages.MOVIE_DESCRIPTION_TOO_LONG)]
        public string Description { get; set; }

        [Required(ErrorMessage = Messages.MOVIE_RELEASE_DATE_REQUIRED)]
        public string ReleaseDate { get; set; }
    }
}
