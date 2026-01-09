using BTKECommerce_Core.DTOs.User;
using BTKECommerce_Core.DTOs.User.ResponseDTO;
using BTKECommerce_Infrastructure.Models;

namespace BTKECommerce_Core.Services.Abstract
{
    public interface IAuthService
    {
        Task<BaseResponseModel<UserDTO>> Register(RegisterDTO dto);
        Task<BaseResponseModel<UserDTO>> Login(LoginDTO dto);
    }
}
