using Assignment.DataAccess.Repositories.Interfaces;
using Assignment.Domain;
using Microsoft.AspNetCore.Mvc;
using MVC_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Assignment.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IRepository<Product> _productRepository;

        public ProductsController(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var products = await _productRepository.All();
            var max = (int)Math.Ceiling(products.Count() / 9.0);
            id = id < 0 ? 0 : id > max ? max : id;
            var model = new ProductIndexModel
            {
                Products = products.Skip((id - 1) * 9).Take(9).Select(_ => new ProductModel { 
                    Id = _.Id,
                    Name = _.Name, 
                    Price = _.Price
                }).ToList()
            };
            ViewBag.Id = id;
            return View(model);
        }

        [HttpGet]
        public IActionResult New()
        {
            var model = new NewProductModel();
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> New([FromForm] NewProductModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var product = new Product() { Name = model.Name, Price = model.Price };

            await _productRepository.Create(product);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productRepository.Single(_ => _.Id == id);
            if (product == null) return RedirectToAction("Index");
            var model = new ProductModel
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Update([FromForm] ProductModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var product = new Product() { Id = model.Id, Name = model.Name, Price = model.Price };
            
            await _productRepository.Update(product);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productRepository.Single(_ => _.Id == id);

            if (product != null)
            {
                await _productRepository.Delete(product);
            }

            return RedirectToAction("Index");
        }

    }
}
