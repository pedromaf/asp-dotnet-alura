using UsuariosAPI.Resources;

namespace UsuariosAPI.Models.Exceptions
{
    public class EmailConfirmationNeededException : Exception
    {
        public EmailConfirmationNeededException() : base(Messages.EMAIL_CONFIRMATION_NEEDED_EXCEPTION)
        {

        }
    }
}
