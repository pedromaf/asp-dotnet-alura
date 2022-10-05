using FilmesAPI.Resources;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs
{
    public class MovieTheaterDTO
    {
        [Required(ErrorMessage = Messages.MOVIETHEATER_NAME_REQUIRED)]
        [StringLength(50, ErrorMessage = Messages.MOVIETHEATER_NAME_TOO_LONG)]
        public string Name { get; set; }

        [Required(ErrorMessage = Messages.MOVIETHEATER_ADDRESSID_REQUIRED)]
        public int AddressId { get; set; }

        [Required(ErrorMessage = Messages.MOVIETHEATER_MANAGERID_REQUIRED)]
        public int ManagerId { get; set; }
    }
}
