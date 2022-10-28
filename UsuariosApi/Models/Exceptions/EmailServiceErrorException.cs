using UsuariosAPI.Resources;

namespace UsuariosAPI.Models.Exceptions
{
    public class EmailServiceErrorException : Exception
    {
        public EmailServiceErrorException() : base(Messages.EMAIL_SERVICE_INTERNAL_ERROR)
        {

        }
    }
}
