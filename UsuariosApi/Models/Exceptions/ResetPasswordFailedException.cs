namespace UsuariosAPI.Models.Exceptions
{
    public class ResetPasswordFailedException : Exception
    {
        public ResetPasswordFailedException() : base(Resources.Messages.RESET_PASSWORD_FAILED)
        {

        }
    }
}
