using BTKECommerce_Core.DTOs.User;
using BTKECommerce_Core.DTOs.User.ResponseDTO;
using BTKECommerce_Core.Services.Abstract;
using BTKECommerce_Domain.Entities;
using BTKECommerce_Infrastructure.Extensions.Token;
using BTKECommerce_Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTKECommerce_Core.Services.Concrete
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;
        private readonly SignInManager<ApplicationUser> _signInManager;
        public AuthService(UserManager<ApplicationUser> userManager, ITokenService tokenService, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _tokenService = tokenService;
            _userManager = userManager;
        }

        public async Task<BaseResponseModel<UserDTO>> Login(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if (user == null)
            {
                return new BaseResponseModel<UserDTO>
                {
                    Success = false,
                    ErrorMessages = new List<string> { "Invalid email or password." },
                    Data = null
                };
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if (!result.Succeeded)
            {
                return new BaseResponseModel<UserDTO>
                {
                    Success = false,
                    ErrorMessages = new List<string> { "Invalid email or password." },
                    Data = null
                };
            }
            var roles = await _userManager.GetRolesAsync(user);
            return new BaseResponseModel<UserDTO>
            {
                Success = true,
                Data = new UserDTO
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    Token = _tokenService.CreateToken(user, roles)
                }


            };
            }  
        public async Task<BaseResponseModel<UserDTO>> Register(RegisterDTO dto)
        {
            BaseResponseModel<UserDTO> response = new BaseResponseModel<UserDTO>();
            var user = new ApplicationUser
            {
                Email = dto.Email,
                UserName = dto.UserName,
                FirstName = dto.FirstName,
                LastName = dto.LastName
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                response.Success = false;
                foreach(var error in result.Errors)
                {
                    response.ErrorMessages.Add(error.Description);
                   
                }
                response.Data = null;
                return response;
            }

            await _userManager.AddToRoleAsync(user, "User");
            var roles = await _userManager.GetRolesAsync(user);
            return new BaseResponseModel<UserDTO>
            {
                Success = true,
                Data = new UserDTO
                {
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Id = user.Id,
                    Token = _tokenService.CreateToken(user, roles)
                }
            };
         
        }
    }
}
