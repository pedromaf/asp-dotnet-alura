using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Models.DTOs;
using UsuariosAPI.Models.Entities;
using UsuariosAPI.Models.Exceptions;
using UsuariosAPI.Models.Requests;
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
                _registrationService.CreateUser(userDTO);

                return Ok();
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpGet("/activate")]
        public IActionResult ActivateUserAccount([FromQuery] ActivateAccountRequest request)
        {
            try
            {
                _registrationService.ActivateAccount(request);

                return Ok();
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }
    }
}
