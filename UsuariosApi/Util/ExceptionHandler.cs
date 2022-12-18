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
                UserRegistrationFailedException => BadRequestResult(controller, exc),
                UserLoginUnauthorizedException => UnauthorizedResult(controller, exc),
                UserLogoutNotPerformedException => UnauthorizedResult(controller, exc),
                EmailConfirmationNeededException => UnauthorizedResult(controller, exc),
                InvalidEmailConfirmationCodeException => UnauthorizedResult(controller, exc),
                UserNotFoundException => BadRequestResult(controller, exc),
                EmailAlreadyInUseException => BadRequestResult(controller, exc),
                ResetPasswordFailedException => UnauthorizedResult(controller, exc),
                _ => InternalServerErrorResult(controller, exc)
            };
        }

        private static IActionResult UnauthorizedResult(ControllerBase controller, Exception exc)
        {
            return controller.StatusCode((int)HttpStatusCode.Unauthorized, exc.Message);
        }

        private static IActionResult BadRequestResult(ControllerBase controller, Exception exc)
        {
            return controller.StatusCode((int)HttpStatusCode.BadRequest, exc.Message);
        }
        private static IActionResult InternalServerErrorResult(ControllerBase controller, Exception exc)
        {
            return controller.StatusCode((int)HttpStatusCode.InternalServerError, exc.Message);
        }
    }
}
