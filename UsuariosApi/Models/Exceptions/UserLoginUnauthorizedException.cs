using UsuariosAPI.Resources;

namespace UsuariosAPI.Models.Exceptions
{
    public class UserLoginUnauthorizedException : Exception
    {
        public UserLoginUnauthorizedException() : base(Messages.LOGIN_UNAUTHORIZED_ERROR)
        {

        }

        public UserLoginUnauthorizedException(String message) : base(message)
        {

        }
    }
}
