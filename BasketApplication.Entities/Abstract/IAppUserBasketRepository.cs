using BasketApplication.Entities.Dtos.Basket;
using BasketApplication.Entities.Dtos.Purchase;
using BasketApplication.Entities.Models;

namespace BasketApplication.Entities.Abstract
{
    public interface IAppUserBasketRepository
    {
        Task<List<BasketProductDto>> GetUserProductsAsync(AppUser user);
        Task<AppUserBasket> AddToBasketAsync(AppUserBasket userBasket);
        Task<AppUserBasket> RemoveFromBasketAsync(AppUser user, int productId);
        Task<AppUserBasket> UpdateFromBasketAsync(AppUser user, int productId, int quantity);
        Task<List<PurchaseHistoryDto>> GetHistory(AppUser user);
        Task<AppUserBasket> OrderBasket(AppUser user);
    }
}
