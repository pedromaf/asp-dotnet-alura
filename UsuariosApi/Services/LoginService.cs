using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Data;
using UsuariosAPI.Models.Requests;
using UsuariosAPI.Models.Exceptions;
using UsuariosAPI.Models.Entities;
using System.ComponentModel;
using Org.BouncyCastle.Asn1.Ocsp;

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
            IdentityUser<int> identityUser = GetUserByUsername(request.Username);

            VerifyIfEmailIsConfirmed(identityUser);

            Login(request.Username, request.Password, false, false);

            return _tokenService.CreateToken(identityUser);
        }

        public Token RequestPasswordReset(RequestingPasswordResetRequest request)
        {
            IdentityUser<int> identityUser = GetUserByEmail(request.Email);

            if (identityUser == null)
            {
                throw new EmailNotRegisteredException();
            }

            string resetTokenValue = _signInManager.UserManager.GeneratePasswordResetTokenAsync(identityUser).Result;

            return new Token(resetTokenValue);
        }

        public void ResetPassword(ResetPasswordRequest request)
        {
            IdentityUser<int> identityUser = GetUserByEmail(request.Email);
            IdentityResult identityResult = _signInManager.UserManager.ResetPasswordAsync(identityUser, request.Token, request.Password).Result;

            if (!identityResult.Succeeded)
            {
                throw new ResetPasswordFailedException();
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

        private IdentityUser<int> GetUserByEmail(string email)
        {
            IdentityUser<int> identityUser = _signInManager.UserManager.Users.FirstOrDefault(user => user.NormalizedEmail == email.ToUpper());

            if (identityUser == null)
            {
                throw new EmailNotRegisteredException();
            }

            return identityUser;
        }

        private IdentityUser<int> GetUserByUsername(string username)
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
    }
}
