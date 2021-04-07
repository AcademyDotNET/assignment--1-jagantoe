using Assignment.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Assignment.Helpers;
using MVC_Assignment.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Assignment.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        private readonly IShopService _shopService;

        public ShopController(IShopService shopService)
        {
            _shopService = shopService;
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            var user = User.GetUser();

            var shoppingBag = await _shopService.GetCart(user);

            if (shoppingBag == null) return RedirectToAction("Index", "Products");

            var discount = _shopService.CalculateDiscount(shoppingBag);

            var model = new CartModel
            {
                Items = shoppingBag.ShoppingItems.Select(_ => new CartItemModel
                {
                    Id = _.Id,
                    Name = _.Product.Name,
                    Price = _.Product.Price,
                    Quantity = _.Quantity
                }).ToList(),
                Discount = discount
            };

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await _shopService.GetProduct(id);

            if (product == null) return RedirectToAction("Index","Products");

            var model = new BuyProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price,
                Quantity = 1
            };

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromForm] BuyProductModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var user = User.GetUser();

            await _shopService.AddToCart(user, model.Id, model.Quantity);

            return RedirectToAction("Index","Products");
        }
    }
}
