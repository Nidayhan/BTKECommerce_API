using Azure;
using BTKECommerce_core.DTOs.User;
using BTKECommerce_core.DTOs.User.ResponseDTO;
using BTKECommerce_core.Services.Abstract;
using BTKECommerce_domain.Entities;
using BTKECommerce_Infrastructure.Extensions.Token;
using BTKECommerce_Infrastructure.Models;
using Microsoft.AspNetCore.Identity;

namespace BTKECommerce_core.Services.Concrate
{
    public class AuthService : IAuthService

    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly ITokenService _tokenService;

        public AuthService(UserManager<ApplicationUser> userManager, ITokenService tokenService, SignInManager<ApplicationUser> signInManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _tokenService = tokenService;
        }
        public async Task<BaseResponseModel<UserDTO>> Login(LoginDTO dto)
        {
            var user = await _userManager.FindByEmailAsync(dto.Email);
            if(user == null)
            {
                return new BaseResponseModel<UserDTO>
                {
                    Success = false,
                    ErrorMessages = new List<string> {"Invalid email or password"},
                    Data = null,
                };
            }
            var result = await _signInManager.CheckPasswordSignInAsync(user, dto.Password, false);
            if(!result.Succeeded)
            {
                return new BaseResponseModel<UserDTO>
                {
                    Success = false,
                    ErrorMessages = new List<string> { "Invalid email or password" },
                    Data = null,
                };
            }
            var roles = await _userManager.GetRolesAsync(user);
            return new BaseResponseModel<UserDTO>
            {
                Success = true,
                Data = new UserDTO
                {
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
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
                LastName = dto.LastName,
            };

            var result = await _userManager.CreateAsync(user, dto.Password);
            if (!result.Succeeded)
            {
                response.Success = false;
                foreach (var error in result.Errors)
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
                    Id = user.Id,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Token = _tokenService.CreateToken(user, roles)
                }
            };
        }
    }
}
