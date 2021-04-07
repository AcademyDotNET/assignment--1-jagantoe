using Assignment.Logic;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MVC_Assignment.Mail;
using MVC_Assignment.Models;
using System.Threading.Tasks;

namespace MVC_Assignment.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IMailer _mailer;
        private readonly SmtpSettings _smtpSettings;

        public UserController(IUserService userService, IMailer mailer, SmtpSettings smtpSettings)
        {
            _userService = userService;
            _mailer = mailer;
            _smtpSettings = smtpSettings;
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

            await SendWelcomeMail(model.FirstName, model.Email);

            return RedirectToAction("Login");
        }

        private async Task SendWelcomeMail(string firstName, string email)
        {
            var mailToUser = new MimeMessage();
            mailToUser.To.Add(MailboxAddress.Parse(email));
            mailToUser.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.Sender));
            mailToUser.Subject =  $"Bike Shop Registration";

            var bodyBuilder = new BodyBuilder()
            {
                HtmlBody = $"<html><body>" +
                           $"<h1>Welcome {firstName}</h1> " +
                           $"<p>Your account has been created and you can now access the shop.</p>" +
                           $"</body></html>"
            };
            mailToUser.Body = bodyBuilder.ToMessageBody();
            await _mailer.SendMailAsync(mailToUser);
        }
    }
}
