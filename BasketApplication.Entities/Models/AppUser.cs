using Microsoft.AspNetCore.Identity;

namespace BasketApplication.Entities.Models
{
    public class AppUser : IdentityUser
    {
        public List<AppUserBasket> AppUserBasket { get; set; } = new List<AppUserBasket>();
    }
}
