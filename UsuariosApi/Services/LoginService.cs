using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data;
using UsuariosAPI.Models.Requests;
using UsuariosAPI.Models.Exceptions;
using UsuariosAPI.Models.Entities;
using System.ComponentModel;

namespace UsuariosAPI.Services
{
    public class LoginService
    {
        private readonly SignInManager<IdentityUser<int>> _signInManager;
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly TokenService _tokenService;

        public LoginService(SignInManager<IdentityUser<int>> signInManager, UserManager<IdentityUser<int>> userManager, TokenService tokenService)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }

        public Token UserLogin(LoginRequest request)
        {
            IdentityUser<int> identityUser = GetIdentityUser(request.Username);

            VerifyIfEmailIsConfirmed(identityUser);

            Login(request.Username, request.Password, false, false);

            Token token = _tokenService.CreateToken(identityUser);

            return token;
        }

        private IdentityUser<int> GetIdentityUser(string username)
        {
            IdentityUser<int> identityUser = _signInManager.UserManager.Users.FirstOrDefault(
                        user => user.NormalizedUserName == username.ToUpper()
                    );

            if (identityUser == null)
            {
                throw new UserLoginUnauthorizedException();
            }

            return identityUser;
        }

        private void VerifyIfEmailIsConfirmed(IdentityUser<int> user)
        {
            var isEmailConfirmed = _userManager.IsEmailConfirmedAsync(user);

            if (isEmailConfirmed.Result == false)
            {
                throw new EmailConfirmationNeededException();
            }
        }

        private void Login(string username, string password, bool isPersistent, bool lockoutOnFailure)
        {
            Task<SignInResult> resultIdentity = _signInManager.PasswordSignInAsync(username, password, isPersistent, lockoutOnFailure);

            if (!resultIdentity.Result.Succeeded)
            {
                throw new UserLoginUnauthorizedException();
            }
        }
    }
}
