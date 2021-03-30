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
        public List<ProductModel> Products { get; set; } = new List<ProductModel>() {
                new ProductModel { Id = 1, Name = "Mountain Bike", Price = 500 },
                new ProductModel { Id = 2, Name = "Very Cool Bike", Price = 700 },
                new ProductModel { Id = 3, Name = "Very Fast Bike", Price = 950 },
                new ProductModel { Id = 4, Name = "Just a Bike", Price = 300 },
                new ProductModel { Id = 5, Name = "Solid Steel Bike", Price = 2000 },
                new ProductModel { Id = 6, Name = "Strong Bike", Price = 600 },
                new ProductModel { Id = 4, Name = "Bike Tire", Price = 100 },
                new ProductModel { Id = 5, Name = "Air Pump", Price = 50 },
                new ProductModel { Id = 6, Name = "Water Bottle", Price = 10 },
            };

        public IActionResult Index()
        {
            var model = new ProductIndexModel
            {
                Products = Products
            };
            return View(model);
        }

        public IActionResult Detail(int id)
        {
            var model = Products.Single(_ => _.Id == id);
            return View(model);
        }
    }
}
