using BTKECommerce_Core.DTOs.User;
using BTKECommerce_Core.Services.Abstract;
using BTKECommerce_Domain.Entities;
using BTKECommerce_Infrastructure.Extensions.Token;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace BTKECommerce_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ITokenService tokenService;
        public AuthController(IAuthService authService,ITokenService tokenService)
        {
            this.tokenService = tokenService;
            _authService = authService;
        }

        [HttpPost("Register")]
        public async Task<IActionResult> SignUp(RegisterDTO dto)
        {
            var result = await _authService.Register(dto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);


        }


        [HttpPost("Login")]
        [EnableRateLimiting("basic")]
        public async Task<IActionResult> SignIn(LoginDTO dto)
        {
            var result = await _authService.Login(dto);
            if (!result.Success)
            {
                return BadRequest(result);
            }
            return Ok(result);

        }

            [HttpPost("GenerateToke")]
        public async Task<IActionResult> GenerateToken()
        {
            var user = new ApplicationUser
            {
                UserName = "testuser",
                Email = string.Empty,
                Id = Guid.NewGuid().ToString()

            };
            var roles = new List<string> { "Admin", "User" };

            var res = tokenService.CreateToken(user,roles);
            return Ok();
        }
    }
}