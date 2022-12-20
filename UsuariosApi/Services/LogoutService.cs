using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Models.Entities;
using UsuariosAPI.Models.Exceptions;

namespace UsuariosAPI.Services
{
    public class LogoutService
    {
        private readonly SignInManager<CustomIdentityUser> _signInManager;

        public LogoutService(SignInManager<CustomIdentityUser> signInManager)
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
