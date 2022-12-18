using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Models.Requests
{
    public class ResetPasswordRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
