using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Models.DTOs;
using UsuariosAPI.Models.Exceptions;
using UsuariosAPI.Services;
using UsuariosAPI.Util;

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

        [HttpPost]
        public IActionResult RegisterUser([FromBody] CreateUserDTO userDTO)
        {
            try
            {
                _registrationService.CreateUser(userDTO);

                return Ok();
            }
            catch(UserRegistrationFailedException exc) { return this.HandleException(exc); }
        }
    }
}
