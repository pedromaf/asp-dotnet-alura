using Microsoft.EntityFrameworkCore.Query.Internal;
using UsuariosAPI.Resources;

namespace UsuariosAPI.Models.Exceptions
{
    public class UserLogoutNotPerformedException : Exception
    {
        public UserLogoutNotPerformedException() : base(Messages.USER_LOGOUT_NOT_PERFORMED_ERROR)
        {

        }

        public UserLogoutNotPerformedException(string message) : base(message)
        {

        }
    }
}
