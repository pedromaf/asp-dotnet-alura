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
        private readonly UserManager<CustomIdentityUser> _userManager;
        private readonly EmailService _emailService;

        public RegistrationService(IMapper mapper, UserManager<CustomIdentityUser> userManager, EmailService emailService)
        {
            _mapper = mapper;
            _userManager = userManager;
            _emailService = emailService;
        }

        public void CreateUser(CreateUserDTO userDTO)
        {
            VerifyEmailDuplicity(userDTO.Email);

            bool emailSent;
            User newUser = _mapper.Map<User>(userDTO);
            CustomIdentityUser identityUser = _mapper.Map<CustomIdentityUser>(newUser);
            IdentityResult creationResult = _userManager.CreateAsync(identityUser, userDTO.Password).Result;

            _userManager.AddToRoleAsync(identityUser, "regular-user");

            if (creationResult.Succeeded)
            {
                Task<string> confirmationCode = _userManager.GenerateEmailConfirmationTokenAsync(identityUser);
                EmailConfirmationCode code = new EmailConfirmationCode(confirmationCode.Result);

                emailSent = _emailService.SendAccountConfirmationEmail(identityUser.Email, identityUser.Id, code);
            } 
            else
            {
                IdentityError error = creationResult.Errors.FirstOrDefault();

                throw new UserRegistrationFailedException(error.Description ?? Messages.USER_REGISTRATION_FAILED);
            }

            if(!emailSent)
            {
                IdentityResult deleteResult = _userManager.DeleteAsync(identityUser).Result;

                throw new EmailServiceErrorException();
            }
        }

        private void VerifyEmailDuplicity(string email)
        {
            if (_userManager.Users.Any(user => user.NormalizedEmail == email.ToUpper()))
            {
                throw new EmailAlreadyInUseException();
            }
        }

        public void ActivateAccount(ActivateAccountRequest request)
        {
            CustomIdentityUser identityUser = GetIdentityUserById(request.UserId);
            IdentityResult identityResult = _userManager.ConfirmEmailAsync(identityUser, request.ActivationCode).Result;

            if(!identityResult.Succeeded)
            {
                throw new InvalidEmailConfirmationCodeException();
            }
        }

        private CustomIdentityUser GetIdentityUserById(int id)
        {
            CustomIdentityUser identityUser = _userManager.Users.FirstOrDefault(user => user.Id == id);

            if (identityUser == null)
            {
                throw new UserNotFoundException();
            }

            return identityUser;
        }
    }
}
