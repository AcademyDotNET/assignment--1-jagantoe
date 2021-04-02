using Assignment.DataAccess.Repositories.Interfaces;
using Assignment.Domain;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MVC_Assignment.Helpers;
using MVC_Assignment.Models;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MVC_Assignment.Controllers
{
    public class UserController : Controller
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<ShoppingBag> _shoppingBagRepository;
        private readonly IPasswordHasher _passwordHasher;
        public UserController(IRepository<Customer> customerRepository, 
            IRepository<ShoppingBag> shoppingbagRespository,
            IPasswordHasher passwordHasher)
        {
            _customerRepository = customerRepository;
            _shoppingBagRepository = shoppingbagRespository;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromForm] LoginModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var customer = await _customerRepository.Single(_ => _.Email == model.Email);
            if (customer == null)
            {
                ViewBag.Message = "No user found for that email";
                return View();
            }
            var valid = _passwordHasher.CheckPassword(model.Password, customer.PasswordHash, customer.PasswordSalt);
            if (!valid)
            {
                ViewBag.Message = "Invalid password";
                return View();
            }

            var claims = new List<Claim>
            {
                new Claim("user", customer.Email)
            };

            var identity = new ClaimsIdentity(claims, "claims");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(principal);

            return RedirectToAction("Index", "Products");
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register([FromForm] RegisterModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var shoppingBag = new ShoppingBag(DateTime.UtcNow);
            await _shoppingBagRepository.Create(shoppingBag);
            var passwordHash = _passwordHasher.GenerateHashedPassword(model.Password);
            var customer = new Customer(model.FirstName, model.LastName, model.Email, passwordHash.Password, passwordHash.Salt, shoppingBag.Id);
            await _customerRepository.Create(customer);

            return RedirectToAction("Login");
        }
    }
}
