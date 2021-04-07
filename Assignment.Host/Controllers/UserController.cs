using Assignment.Logic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MVC_Assignment.Models;
using System.Threading.Tasks;

namespace MVC_Assignment.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
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

            var login = await _userService.Login(model.Email, model.Password);

            if (login == null)
            {
                ViewBag.Message = "Invalid email/password";
                return View();
            }

            await HttpContext.SignInAsync(login);

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

            var response = await _userService.Register(model.FirstName, model.LastName, model.Email, model.Password);

            if (!response)
            {
                ViewBag.Message = "Customer already exists.";
                return View();
            }

            return RedirectToAction("Login");
        }
    }
}
