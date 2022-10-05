using FilmesAPI.Resources;
using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Models.Entities
{
    public class Manager
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = Messages.MANAGER_NAME_REQUIRED)]
        [StringLength(50, ErrorMessage = Messages.MANAGER_NAME_TOO_LONG)]
        public string Name { get; set; }

        public virtual List<MovieTheater> MovieTheaters { get; set; }
    }
}
