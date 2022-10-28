using System.ComponentModel.DataAnnotations;

namespace UsuariosAPI.Models.Requests
{
    public class ActivateAccountRequest
    {
        [Required]
        public int UserId { get; set; }

        [Required]
        public string ActivationCode { get; set; }
    }
}
