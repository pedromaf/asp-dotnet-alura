using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Query.Internal;
using UsuariosAPI.Models.Entities;
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
            catch (ArgumentException exc) { return this.HandleException(exc); }
            catch (UserLoginUnauthorizedException exc) { return this.HandleException(exc); }
            catch (EmailConfirmationNeededException exc) { return this.HandleException(exc); }
        }
    }
}
