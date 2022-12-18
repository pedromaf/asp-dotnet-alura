using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Models.Entities;
using UsuariosAPI.Models.Requests;
using UsuariosAPI.Services;
using UsuariosAPI.Util;

namespace UsuariosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult UserLogin([FromBody] LoginRequest request)
        {
            try
            {
                Token token = _loginService.UserLogin(request);

                return Ok(token);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpPost("reset-password-request")]
        public IActionResult RequestPasswordReset(RequestingPasswordResetRequest request)
        {
            try
            {
                Token token = _loginService.RequestPasswordReset(request);

                return Ok(token);
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }

        [HttpPost("reset-password")]
        public IActionResult ResetPassword(ResetPasswordRequest request)
        {
            try
            {
                _loginService.ResetPassword(request);

                return Ok();
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }
    }
}
