using Assignment.DataAccess.Repositories.Interfaces;
using Assignment.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Assignment.Helpers;
using MVC_Assignment.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Assignment.Controllers
{
    [Authorize]
    public class ShopController : Controller
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ShoppingBag> _shoppingBagRepository;
        private readonly IRepository<ShoppingItem> _shoppingItemRepository;
        private readonly IRepository<Customer> _customerRepository;

        public ShopController(IRepository<Product> productRepository, 
            IRepository<ShoppingBag> shoppingBagRepository, 
            IRepository<ShoppingItem> shoppingItemRepository, 
            IRepository<Customer> customerRepository)
        {
            _productRepository = productRepository;
            _shoppingBagRepository = shoppingBagRepository;
            _shoppingItemRepository = shoppingItemRepository;
            _customerRepository = customerRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            var user = User.GetUser();

            var shoppingBag = await _shoppingBagRepository.Single(_ => _.Customer.Email == user,
                (IQueryable<ShoppingBag> subjects) => subjects.Include(_ => _.ShoppingItems).ThenInclude(_ => _.Product));

            if (shoppingBag == null) return RedirectToAction("Index", "Products");

            var model = new CartModel
            {
                Items = shoppingBag.ShoppingItems.Select(_ => new CartItemModel
                {
                    Id = _.Id,
                    Name = _.Product.Name,
                    Price = _.Product.Price,
                    Quantity = _.Quantity
                }).ToList()
            };
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await _productRepository.Single(_ => _.Id == id);
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

            var customer = await _customerRepository.Single(_ => _.Email == user);

            var shoppingItem = new ShoppingItem(model.Quantity, model.Id, customer.ShoppingBagId);

            await _shoppingItemRepository.Create(shoppingItem);

            return RedirectToAction("Index","Products");
        }
    }
}
