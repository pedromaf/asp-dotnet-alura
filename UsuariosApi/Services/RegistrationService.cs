using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Models.DTOs;
using UsuariosAPI.Models.Entities;
using UsuariosAPI.Models.Exceptions;
using UsuariosAPI.Models.Requests;
using UsuariosAPI.Resources;

namespace UsuariosAPI.Services
{
    public class RegistrationService
    {
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser<int>> _userManager;
        private readonly EmailService _emailService;

        public RegistrationService(IMapper mapper, UserManager<IdentityUser<int>> userManager, EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public void CreateUser(CreateUserDTO userDTO)
        {
            User newUser = _mapper.Map<User>(userDTO);
            IdentityUser<int> identityUser = _mapper.Map<IdentityUser<int>>(newUser);
            Task<IdentityResult> identityResult = _userManager.CreateAsync(identityUser, userDTO.Password);

            if(identityResult.Result.Succeeded)
            {
                Task<string> confirmationCode = _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                EmailConfirmationCode code = new EmailConfirmationCode(confirmationCode.Result);

                _emailService.SendAccountConfirmationEmail(identityUser.Email, identityUser.Id, code);
            } else
            {
                IdentityError error = identityResult.Result.Errors.FirstOrDefault();

                throw new UserRegistrationFailedException(error.Description ?? Messages.USER_REGISTRATION_FAILED);
            }
        }

        public void ActivateAccount(ActivateAccountRequest request)
        {
            IdentityUser<int> identityUser = GetIdentityUserById(request.UserId);
            IdentityResult identityResult = _userManager.ConfirmEmailAsync(identityUser, request.ActivationCode).Result;

            if(!identityResult.Succeeded)
            {
                throw new InvalidEmailConfirmationCodeException();
            }
        }

        private IdentityUser<int> GetIdentityUserById(int id)
        {
            IdentityUser<int> identityUser = _userManager.Users.FirstOrDefault(user => user.Id == id);

            if (identityUser == null)
            {
                throw new UserNotFoundException();
            }

            return identityUser;
        }
    }
}
