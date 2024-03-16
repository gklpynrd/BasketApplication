using BasketApplication.Entities.Abstract;
using BasketApplication.Entities.Dtos.Basket;
using BasketApplication.Entities.Dtos.ProductDto;
using BasketApplication.Entities.Dtos.Purchase;
using BasketApplication.Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace BasketApplication.DataAccess.Repository
{
    public class AppUserBasketRepository : IAppUserBasketRepository
    {
        private readonly ApplicationDBContext _context;

        public AppUserBasketRepository(ApplicationDBContext context)
        {
            _context = context;
        }

        public async Task<AppUserBasket> AddToBasketAsync(AppUserBasket userBasket)
        {
            var product = await _context.Products.Include(x => x.Stock).FirstOrDefaultAsync(x => x.Id == userBasket.ProductId);
            if (product == null || product.Stock.Quantity < userBasket.Quantity)
            {
                return null;
            }
            await _context.AppUserBaskets.AddAsync(userBasket);
            await _context.SaveChangesAsync();
            return userBasket;
        }

        public async Task<List<PurchaseHistoryDto>> GetHistory(AppUser user)
        {
            var res = await _context.PurchaseHistories.Include(x => x.Details).ThenInclude(x => x.Product).Where(x => x.UserId == user.Id).OrderByDescending(x => x.PurchaseDate).ToListAsync();

            var history = new List<PurchaseHistoryDto>();

            foreach (var item in res)
            {
                history.Add(new PurchaseHistoryDto
                {
                    PurchaseDate = item.PurchaseDate,
                    PurchaseId = item.PurchaseId,
                    UserName = item.User.UserName,
                    TotalPrice = item.Details.Sum(x => x.Price * x.Quantity),
                    Products = item.Details.Select(x => new HistoryProductDto
                    {
                        Id = x.ProductId,
                        Name = x.Product.Name,
                        Price = x.Price,
                        Quantity = x.Quantity,
                    }).ToList(),
                });
            }


            return history;

        }

        public async Task<List<BasketProductDto>> GetUserProductsAsync(AppUser user)
        {
            return await _context.AppUserBaskets.Where(x => x.AppUserId == user.Id && x.Status == true)
                 .Select(product => new BasketProductDto
                 {
                     //Id = product.ProductId,
                     //Name = product.Product.Name,
                     //Price = product.Product.Price,
                     Id = product.ProductId,
                     Name = product.Product.Name,
                     Price = product.Product.Price,
                     Quantity = product.Quantity,
                     TotalProductPrice = product.Product.Price * product.Quantity,

                 }).ToListAsync();
        }

        public async Task<AppUserBasket> OrderBasket(AppUser user)
        {
            var transaction = _context.Database.BeginTransaction();
            var basket = await _context.AppUserBaskets.Where(x => x.AppUserId == user.Id && x.Status == true).Include(x => x.Product).ThenInclude(x => x.Stock).ToListAsync();
            var history = new PurchaseHistory
            {
                UserId = user.Id,
                TotalPrice = basket.Sum(x => x.Product.Price * x.Quantity),
                PurchaseDate = DateTime.Now,
                DetailsId = 1,
            };
            await _context.PurchaseHistories.AddAsync(history);
            await _context.SaveChangesAsync();

            foreach (var item in basket)
            {
                if (item.Quantity < item.Product.Stock.Quantity)
                {
                    return null;
                }
                _context.PurchaseDetails.Add(new PurchaseDetails
                {
                    PurchaseId = history.PurchaseId,
                    Price = item.Product.Price,
                    ProductId = item.ProductId,
                    Quantity = item.Quantity,
                });
            }

            _context.AppUserBaskets.RemoveRange(basket);

            await _context.SaveChangesAsync();

            transaction.Commit();

            return null;

        }

        public async Task<AppUserBasket> RemoveFromBasketAsync(AppUser user, int productId)
        {
            var basketModel = await _context.AppUserBaskets.FirstOrDefaultAsync(x => x.AppUserId == user.Id && x.ProductId == productId);
            if (basketModel == null)
            {
                return null;
            }

            _context.AppUserBaskets.Remove(basketModel);
            await _context.SaveChangesAsync();
            return basketModel;
        }

        public async Task<AppUserBasket> UpdateFromBasketAsync(AppUser user, int productId, int quantity)
        {
            var basketModel = await _context.AppUserBaskets.FirstOrDefaultAsync(x => x.AppUserId == user.Id && x.ProductId == productId);
            if (basketModel == null)
            {
                return null;
            }

            basketModel.Quantity = quantity;
            await _context.SaveChangesAsync();
            return basketModel;
        }
    }
}
