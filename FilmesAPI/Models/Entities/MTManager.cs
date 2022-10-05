using FilmesAPI.Resources;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace FilmesAPI.Models.Entities
{
    public class MTManager
    {
        [Key]
        [Required]
        public int Id { get; set; }

        [Required(ErrorMessage = Messages.MANAGER_NAME_REQUIRED)]
        [StringLength(50, ErrorMessage = Messages.MANAGER_NAME_TOO_LONG)]
        public string Name { get; set; }
       
        [JsonIgnore]
        public virtual List<MovieTheater> MovieTheaters { get; set; }
    }
}
