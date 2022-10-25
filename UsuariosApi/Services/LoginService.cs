using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data;
using UsuariosAPI.Models.Requests;
using UsuariosAPI.Models.Exceptions;
using UsuariosAPI.Models.Entities;

namespace UsuariosAPI.Services
{
    public class LoginService
    {
        private readonly SignInManager<IdentityUser<int>> _signInManager;

        public LoginService(SignInManager<IdentityUser<int>> signInManager)
        {
            _signInManager = signInManager;
        }

        public string UserLogin(LoginRequest request)
        {
            Task<SignInResult> resultIdentity = _signInManager.PasswordSignInAsync(request.Username, request.Password, false, false);

            if (resultIdentity.Result.Succeeded)
            {
                IdentityUser<int> identityUser = _signInManager.UserManager.Users.FirstOrDefault(
                        user => user.NormalizedUserName == request.Username.ToUpper()
                    );
                Token token = TokenService.CreateToken(identityUser);
                
                return token.Value;
            } 
            else
            {
                throw new UserLoginUnauthorizedException();
            }
        }
    }
}
