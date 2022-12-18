using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Models.Exceptions;
using UsuariosAPI.Services;
using UsuariosAPI.Util;

namespace UsuariosAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LogoutController : ControllerBase
    {
        private readonly LogoutService _logoutService;

        public LogoutController(LogoutService logoutService)
        {
            _logoutService = logoutService;
        }

        [HttpPost]
        public IActionResult UserLogout()
        {
            try
            {
                _logoutService.UserLogout();

                return Ok();
            }
            catch (Exception exc) { return this.HandleException(exc); }
        }
    }
}
