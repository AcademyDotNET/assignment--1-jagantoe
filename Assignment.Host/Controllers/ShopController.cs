using Assignment.Common.Logger;
using Assignment.Common.Mail;
using Assignment.Logic;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
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
        private readonly IMailer _mailer;
        private readonly SmtpSettings _smtpSettings;
        private readonly ICustomLogger _logger;

        public ShopController(IShopService shopService, IMailer mailer, SmtpSettings smtpSettings, ICustomLogger logger)
        {
            _shopService = shopService;
            _mailer = mailer;
            _smtpSettings = smtpSettings;
            _logger = logger;
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
        public async Task<IActionResult> PlaceOrder()
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

            await SendOrderEmail(user, model);
            _logger.Log($"{user} has placed an order for the following items:", model.Items);
            return View();
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



        private async Task SendOrderEmail(string email, CartModel cartModel)
        {
            var mailToUser = new MimeMessage();
            mailToUser.To.Add(MailboxAddress.Parse(email));
            mailToUser.From.Add(new MailboxAddress(_smtpSettings.SenderName, _smtpSettings.Sender));
            mailToUser.Subject = $"Bike Shop Order";

            var bodyBuilder = new BodyBuilder()
            {
                HtmlBody = $"<html><body>" +
                           $"<h1>Your order is being processed</h1> " +
                           $"<h2>Overview:</h2>" +
                           $"<ul>" +
                           string.Join("", cartModel.Items.Select(_ => $"<li>{_.Quantity} {_.Name} </li>")) +
                           $"</ul>" +
                           $"<h3>Total value: {cartModel.Items.Sum(_ => _.Quantity * _.Price)}</h3>" +
                           $"<h3>Discount:{cartModel.Discount}</h3>" +
                           $"<h2>Total to pay: {cartModel.Items.Sum(_ => _.Quantity * _.Price) - cartModel.Discount}</h2>" +
                           $"</body></html>"
            };
            mailToUser.Body = bodyBuilder.ToMessageBody();
            await _mailer.SendMailAsync(mailToUser);
        }
    }
}
