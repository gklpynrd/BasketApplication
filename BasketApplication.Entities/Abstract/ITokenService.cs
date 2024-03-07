using BasketApplication.Entities.Models;

namespace BasketApplication.Entities.Abstract
{
    public interface ITokenService
    {
        string CreateToken(AppUser appUser);
    }
}
