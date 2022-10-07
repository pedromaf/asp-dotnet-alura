using FilmesAPI.Resources;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs
{
    public class MovieSessionDTO
    {
        [Required(ErrorMessage = Messages.MOVIESESSION_MOVIEID_REQUIRED)]
        public int MovieId { get; set; }

        [Required(ErrorMessage = Messages.MOVIESESSION_MOVIETHEATERID_REQUIRED)]
        public int MovieTheaterId { get; set; }

        [Required(ErrorMessage = Messages.MOVIESESSION_START_REQUIRED)]
        public DateTime Start { get; set; }

        [Required(ErrorMessage = Messages.MOVIESESSION_END_REQUIRED)]
        public DateTime End { get; set; }
    }
}
