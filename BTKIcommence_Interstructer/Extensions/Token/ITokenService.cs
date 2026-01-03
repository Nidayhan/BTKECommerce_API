using BTKECommerce_domain.Entities;

namespace BTKECommerce_Infrastructure.Extensions.Token
{
    public interface ITokenService
    {
        string CreateToken(ApplicationUser user, IList<string> roles);
    }
}
