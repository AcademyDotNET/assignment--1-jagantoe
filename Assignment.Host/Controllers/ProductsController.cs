using Assignment.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MVC_Assignment.Models;
using System.Linq;
using System.Threading.Tasks;

namespace MVC_Assignment.Controllers
{
    [Authorize]
    public class ProductsController : Controller
    {
        private readonly IProductService _productService;

        public ProductsController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            var products = await _productService.GetProductsList(id);
            var model = new ProductIndexModel
            {
                Products = products.Select(_ => new ProductModel { 
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

            await _productService.CreateProduct(model.Name, model.Price);

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var product = await _productService.GetProduct(id);
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

            await _productService.UpdateProduct(model.Id, model.Name, model.Price);

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _productService.DeleteProduct(id);

            return RedirectToAction("Index");
        }
    }
}
