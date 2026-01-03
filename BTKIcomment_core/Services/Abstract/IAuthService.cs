using BTKECommerce_core.DTOs.User;
using BTKECommerce_core.DTOs.User.ResponseDTO;
using BTKECommerce_Infrastructure.Models;

namespace BTKECommerce_core.Services.Abstract
{
    public interface IAuthService
    {
        Task<BaseResponseModel<UserDTO>> Register(RegisterDTO dto);
        Task<BaseResponseModel<UserDTO>> Login(LoginDTO dto);
    }
}
