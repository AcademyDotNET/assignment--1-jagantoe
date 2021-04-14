using Assignment.DataAccess.Repositories.Interfaces;
using Assignment.Domain;
using Assignment.Logic.Helpers;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Logic
{
    public interface IShopService
    {
        Task<ShoppingBag> GetCart(string userEmail);
        Task<Product> GetProduct(int productId);
        Task AddToCart(string userEmail, int productId, int amount);
        int CalculateDiscount(ShoppingBag shoppingBag);
    }

    public class ShopService : IShopService
    {
        private readonly IRepository<Product> _productRepository;
        private readonly IRepository<ShoppingBag> _shoppingBagRepository;
        private readonly IRepository<ShoppingItem> _shoppingItemRepository;
        private readonly IRepository<Customer> _customerRepository;
        private readonly IDiscountCalculator _discountCalculator;

        public ShopService(
            IRepository<Product> productRepository, 
            IRepository<ShoppingBag> shoppingBagRepository, 
            IRepository<ShoppingItem> shoppingItemRepository, 
            IRepository<Customer> customerRepository, 
            IDiscountCalculator discountCalculator)
        {
            _productRepository = productRepository;
            _shoppingBagRepository = shoppingBagRepository;
            _shoppingItemRepository = shoppingItemRepository;
            _customerRepository = customerRepository;
            _discountCalculator = discountCalculator;
        }

        public async Task<ShoppingBag> GetCart(string userEmail)
        {
            return await _shoppingBagRepository.Single(_ => _.Customer.Email == userEmail, subjects => subjects.Include(_ => _.ShoppingItems).ThenInclude(_ => _.Product));
        }

        public Task<Product> GetProduct(int productId)
        {
            return _productRepository.Single(_ => _.Id == productId);
        }

        public async Task AddToCart(string userEmail, int productId, int amount)
        {
            var customer = await _customerRepository.Single(_ => _.Email == userEmail);

            var shoppingItem = new ShoppingItem(productId, amount, customer.ShoppingBagId);

            await _shoppingItemRepository.Create(shoppingItem);
        }

        public int CalculateDiscount(ShoppingBag shoppingBag)
        {
            return _discountCalculator.Calculate(shoppingBag.ShoppingItems);
        }
    }
}
