using Microsoft.AspNetCore.Mvc;
using System.Net;
using UsuariosAPI.Models.Exceptions;

namespace UsuariosAPI.Util
{
    public static class ExceptionHandler
    {
        public static IActionResult HandleException(this ControllerBase controller, Exception exc)
        {
            return exc switch
            {
                UserRegistrationFailedException => controller.StatusCode((int)HttpStatusCode.BadRequest, exc.Message),
                _ => controller.StatusCode((int)HttpStatusCode.InternalServerError, exc.Message)
            };
        }
    }
}
