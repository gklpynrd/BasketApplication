using BasketApplication.Entities.Abstract;
using BasketApplication.Entities.Dtos.Basket;
using BasketApplication.Entities.Models;
using BasketApplication.Helpers.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BasketApplication.WebAPI.Controllers
{
    [Route("api/userbasket")]
    [Authorize]
    public class AppUserBasketController : Controller
    {
        private readonly IAppUserBasketRepository _userBasketRepository;
        private readonly UserManager<AppUser> _userManager;

        public AppUserBasketController(IAppUserBasketRepository userBasketRepository, UserManager<AppUser> userManager)
        {
            _userBasketRepository = userBasketRepository;
            _userManager = userManager;
        }

        [HttpGet]
        [Route("currentbasket")]
        public async Task<IActionResult> CurrentBasket()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var userbasket = await _userBasketRepository.GetUserProductsAsync(appUser);

            return Ok(new UserBasketDto { Username = username, Products = userbasket, TotalBasketPrice = userbasket.Sum(x => x.TotalProductPrice) });
        }

        [HttpPost]
        [Route("addproducttobasket")]
        public async Task<IActionResult> AddProductToCurrentBasket([FromBody] AddProductToBasketDto productDto)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            var UserBasket = new AppUserBasket
            {
                AppUserId = appUser.Id,
                ProductId = productDto.ProductId,
                Quantity = productDto.Quantity,
                Status = true,
            };

            await _userBasketRepository.AddToBasketAsync(UserBasket);

            return Ok();

        }

        [HttpDelete]
        [Route("deleteproductfrombasket")]
        public async Task<IActionResult> DeleteProductFromBasket(int productId)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            await _userBasketRepository.RemoveFromBasketAsync(appUser, productId);
            return Ok();
        }

        [HttpPut]
        [Route("updateproductfrombasket/{productId}/{quantity}")]
        public async Task<IActionResult> updateProductFromBasket([FromRoute] int productId, int quantity)
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);
            await _userBasketRepository.UpdateFromBasketAsync(appUser, productId, quantity);
            return Ok();
        }

        [HttpGet]
        [Route("orderbasket")]
        public async Task<IActionResult> OrderBasket()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            await _userBasketRepository.OrderBasket(appUser);
            return Ok();
        }

        [HttpGet]
        [Route("PurchaseHistory")]
        public async Task<IActionResult> PurchaseHistory()
        {
            var username = User.GetUsername();
            var appUser = await _userManager.FindByNameAsync(username);

            var res = await _userBasketRepository.GetHistory(appUser);

            return Ok(res);
        }
    }
}
