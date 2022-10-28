using UsuariosAPI.Resources;

namespace UsuariosAPI.Models.Exceptions
{
    public class InvalidEmailConfirmationCodeException : Exception
    {
        public InvalidEmailConfirmationCodeException() : base(Messages.INVALID_EMAIL_CONFIRMATION_CODE)
        {

        }
    }
}
