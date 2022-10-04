using FilmesAPI.Resources;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs.MovieTheater
{
    public class MovieTheaterDTO
    {
        [Required(ErrorMessage = Messages.MOVIETHEATER_NAME_REQUIRED)]
        [StringLength(50, ErrorMessage = Messages.MOVIETHEATER_NAME_TOO_LONG)]
        public string Name { get; set; }
    }
}
