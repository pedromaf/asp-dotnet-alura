using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Models.DTOs
{
    public class CreateUserDTO
    {
        [Required]
        public string Username { get; set; }

        [Required]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string RepeatedPassword { get; set; }
    }
}
