using System.ComponentModel.DataAnnotations;
using FilmesAPI.Resources;

namespace FilmesAPI.Models.Entities
{
    public class Movie
    {
        [Key]
        [Required]
        public int Id { get; set; }

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

        [Required(ErrorMessage = Messages.MOVIE_AGERATING_REQUIRED)]
        [Range(0, 18, ErrorMessage = Messages.MOVIE_AGERATING_OUTOFRANGE)]
        public int AgeRating { get; set; }

        public virtual List<MovieSession> Sessions { get; set; }
    }
}
