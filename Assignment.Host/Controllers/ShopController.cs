using Assignment.DataAccess.Repositories.Interfaces;
using Assignment.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MVC_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Assignment.Controllers
{
    public class ShopController : Controller
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ShoppingBag> _shoppingBagRepository;
        private readonly IRepository<ShoppingItem> _shoppingItemRepository;

        public ShopController(IRepository<Product> productRepository, 
            IRepository<ShoppingBag> shoppingBagRepository, 
            IRepository<ShoppingItem> shoppingItemRepository)
        {
            _productRepository = productRepository;
            _shoppingBagRepository = shoppingBagRepository;
            _shoppingItemRepository = shoppingItemRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Cart()
        {
            var shoppingBag = await _shoppingBagRepository.Single(_ => _.Id == 1,
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
                Quantity = 0
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> AddToCart([FromForm] BuyProductModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var shoppingItem = new ShoppingItem { Quantity = model.Quantity, ProductId = model.Id, ShoppingBagId = 1 };

            await _shoppingItemRepository.Create(shoppingItem);

            return RedirectToAction("Index","Products");
        }
    }
}
