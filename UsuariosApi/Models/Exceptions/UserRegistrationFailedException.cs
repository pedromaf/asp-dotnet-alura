using UsuariosAPI.Resources;

namespace UsuariosAPI.Models.Exceptions
{
    public class UserRegistrationFailedException : Exception
    {
        public UserRegistrationFailedException() : base(Messages.USER_REGISTRATION_FAILED)
        {

        }

        public UserRegistrationFailedException(string message) : base(message)
        {

        }
    }
}
