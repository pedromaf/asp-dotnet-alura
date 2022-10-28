using UsuariosAPI.Resources;

namespace UsuariosAPI.Models.Exceptions
{
    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() : base(Messages.USER_NOT_FOUND)
        {

        }
    }
}
