using Microsoft.AspNetCore.Identity;

namespace UsuariosAPI.Models.Entities
{
    public class CustomIdentityUser : IdentityUser<int>
    {
        public DateTime BirthDate { get; set; }
    }
}
