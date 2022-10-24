using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using UsuariosAPI.Models.Exceptions;
using UsuariosAPI.Models.Requests;
using UsuariosAPI.Services;
using UsuariosAPI.Util;

namespace UsuariosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        private LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult UserLogin([FromBody] LoginRequest request)
        {
            try
            {
                _loginService.UserLogin(request);

                return Ok();
            }
            catch (ArgumentException exc) { return this.HandleException(exc); }
            catch (UserLoginUnauthorizedException exc) { return this.HandleException(exc); }
        }
    }
}
