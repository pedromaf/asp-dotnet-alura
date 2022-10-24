using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data;
using UsuariosAPI.Models.Requests;
using UsuariosAPI.Models.Exceptions;

namespace UsuariosAPI.Services
{
    public class LoginService
    {
        private SignInManager<IdentityUser<int>> _signInManager;

        public LoginService(SignInManager<IdentityUser<int>> signInManager)
        {
            _signInManager = signInManager;
        }

        public void UserLogin(LoginRequest request)
        {
            Task<SignInResult> resultIdentity = _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

            if (!resultIdentity.Result.Succeeded)
            {
                throw new UserLoginUnauthorizedException();
            }
        }
    }
}
