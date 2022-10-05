using FilmesAPI.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace FilmesAPI.Controllers
{
    public static class ExceptionHandler
    {
        public static IActionResult HandleException(this ControllerBase controller, Exception exc)
        {
            return exc switch
            {
                ElementNotFoundException => controller.NotFound(exc.Message),
                _ => controller.StatusCode(500, exc.Message)
            };
        }
    }
}
