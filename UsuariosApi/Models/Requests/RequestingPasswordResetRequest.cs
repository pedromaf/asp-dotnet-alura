using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Models.Requests
{
    public class RequestingPasswordResetRequest
    {
        [Required]
        public string Email { get; set; }
    }
}
