using System.ComponentModel.DataAnnotations;
using UsuariosAPI.Resources;

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
        [Compare("Password", ErrorMessage = Messages.USER_CONFIRMATION_PASSWORD_MATCH_ERROR)]
        public string ConfirmationPassword { get; set; }
    }
}
