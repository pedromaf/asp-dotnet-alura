using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuariosAPI.Data;
using UsuariosAPI.Models.DTOs;
using UsuariosAPI.Models.Entities;
using UsuariosAPI.Models.Exceptions;
using UsuariosAPI.Resources;

namespace UsuariosAPI.Services
{
    public class RegistrationService
    {
        private UserDbContext _context;
        private IMapper _mapper;
        private UserManager<IdentityUser<int>> _userManager;

        public RegistrationService(UserDbContext context, IMapper mapper, UserManager<IdentityUser<int>> userManager)
        {
            _context = context;
            _mapper = mapper;
            _userManager = userManager;
        }

        public void CreateUser(CreateUserDTO userDTO)
        {
            User newUser = _mapper.Map<User>(userDTO);
            IdentityUser<int> identityUser = _mapper.Map<IdentityUser<int>>(newUser);
            Task<IdentityResult> identityResult = _userManager.CreateAsync(identityUser, userDTO.Password);

            if(!identityResult.Result.Succeeded)
            {
                IdentityError error = identityResult.Result.Errors.FirstOrDefault();
                throw new UserRegistrationFailedException(error.Description ?? Messages.USER_REGISTRATION_FAILED);
            }
        }
    }
}
