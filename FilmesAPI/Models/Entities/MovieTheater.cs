using FilmesAPI.Resources;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmesAPI.Models.Entities
{
    public class MovieTheater
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = Messages.MOVIETHEATER_NAME_REQUIRED)]
        [StringLength(50, ErrorMessage = Messages.MOVIETHEATER_NAME_TOO_LONG)]
        public string Name { get; set; }

        [Required(ErrorMessage = Messages.MOVIETHEATER_ADDRESSID_REQUIRED)]
        public int AddressId { get; set; }

        [Required(ErrorMessage = Messages.MOVIETHEATER_MANAGERID_REQUIRED)]
        public int ManagerId { get; set; }

        public virtual Address Address { get; set; }

        public virtual MTManager Manager { get; set; }
        
    }
}
