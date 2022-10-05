using FilmesAPI.Resources;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.DTOs
{
    public class MTManagerDTO
    {
        [Required(ErrorMessage = Messages.MANAGER_NAME_REQUIRED)]
        [StringLength(50, ErrorMessage = Messages.MANAGER_NAME_TOO_LONG)]
        public string Name { get; set; }
    }
}
