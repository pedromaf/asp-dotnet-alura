namespace UsuariosAPI.Models.Exceptions
{
    public class EmailNotRegisteredException : Exception
    {
        public EmailNotRegisteredException() : base(Resources.Messages.EMAIL_NOT_REGISTERED)
        {

        }
    }
}
