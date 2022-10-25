using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Models.Exceptions;

namespace UsuariosAPI.Services
{
    public class LogoutService
    {
        private readonly SignInManager<IdentityUser<int>> _signInManager;

        public LogoutService(SignInManager<IdentityUser<int>> signInManager)
        {
            _signInManager = signInManager;
        }

        internal void UserLogout()
        {
            Task identityResult = _signInManager.SignOutAsync();

            if(!identityResult.IsCompletedSuccessfully)
            {
                throw new UserLogoutNotPerformedException();
            }
        }
    }
}
