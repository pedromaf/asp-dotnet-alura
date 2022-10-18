using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data;

namespace UsuariosAPI.Services
{
    public class RegistrationService
    {
        private UserDbContext _context;

        public RegistrationService(UserDbContext context)
        {
            _context = context;
        }

    }
}
