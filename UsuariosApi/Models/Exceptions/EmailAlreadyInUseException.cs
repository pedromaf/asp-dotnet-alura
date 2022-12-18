namespace UsuariosAPI.Models.Exceptions
{
    public class EmailAlreadyInUseException : Exception
    {
        public EmailAlreadyInUseException() : base(Resources.Messages.EMAIL_ALREADY_IN_USE)
        {

        }
    }
}
