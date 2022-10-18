using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Models.DTOs;
using UsuariosAPI.Services;

namespace UsuariosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class RegistrationController : ControllerBase
    {
        private RegistrationService _registrationService;

        public RegistrationController(RegistrationService service)
        {
            _registrationService = service;
        }
    }
}
