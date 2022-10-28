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
        private readonly RegistrationService _registrationService;

        public RegistrationController(RegistrationService service)
        {
            _registrationService = service;
        }

        [HttpPost]
        public IActionResult RegisterUser([FromBody] CreateUserDTO userDTO)
        {
            try
            {
                string activationCode = _registrationService.CreateUser(userDTO);

                return Ok(activationCode);
            }
            catch(UserRegistrationFailedException exc) { return this.HandleException(exc); }
            catch(AggregateException exc) { return this.HandleException(exc); }
        }

        [HttpPost("/activate")]
        public IActionResult ActivateUserAccount()
        {
            throw new NotImplementedException();
        }
    }
}
