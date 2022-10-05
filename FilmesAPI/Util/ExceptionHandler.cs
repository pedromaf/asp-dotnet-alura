using FilmesAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace FilmesAPI.Controllers
{
    public static class ExceptionHandler
    {
        public static IActionResult HandleException(this ControllerBase controller, Exception exc)
        {
            return exc switch
            {
                ElementNotFoundException => controller.NotFound(exc.Message),
                ElementBeingUsedException => controller.StatusCode((int)HttpStatusCode.BadRequest, exc.Message),
                _ => controller.StatusCode((int)HttpStatusCode.InternalServerError, exc.Message)
            };
        }
    }
}
