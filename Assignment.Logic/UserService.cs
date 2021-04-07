using Assignment.DataAccess.Repositories.Interfaces;
using Assignment.Domain;
using Assignment.Logic.Helpers;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Assignment.Logic
{
    public interface IUserService
    {
        Task<ClaimsPrincipal> Login(string email, string password);
        Task<bool> Register(string firstName, string lastName, string email, string password);
    }

    public class UserService : IUserService
    {
        private readonly IRepository<Customer> _customerRepository;
        private readonly IRepository<ShoppingBag> _shoppingBagRepository;
        private readonly IPasswordHasher _passwordHasher;
        public UserService(IRepository<Customer> customerRepository, IRepository<ShoppingBag> shoppingbagRespository, IPasswordHasher passwordHasher)
        {
            _customerRepository = customerRepository;
            _shoppingBagRepository = shoppingbagRespository;
            _passwordHasher = passwordHasher;
        }

        public async Task<ClaimsPrincipal> Login(string email, string password)
        {
            var customer = await _customerRepository.Single(_ => _.Email == email);
            
            if (customer == null)
            {
                return null;
            }

            var valid = _passwordHasher.CheckPassword(password, customer.PasswordHash, customer.PasswordSalt);
            
            if (!valid)
            {
                return null;
            }

            var claims = new List<Claim>
            {
                new Claim("user", customer.Email)
            };

            var identity = new ClaimsIdentity(claims, "claims");

            var principal = new ClaimsPrincipal(identity);

            return principal;
        }

        public async Task<bool> Register(string firstName, string lastName, string email, string password)
        {
            var customerExists = await _customerRepository.Single(_ => _.FirstName == firstName && _.LastName == lastName);

            if (customerExists != null)
            {
                return false;
            }

            var shoppingBag = new ShoppingBag(DateTime.UtcNow);

            await _shoppingBagRepository.Create(shoppingBag);

            var passwordHash = _passwordHasher.GenerateHashedPassword(password);

            var customer = new Customer(firstName, lastName, email, passwordHash.Password, passwordHash.Salt, shoppingBag.Id);
            
            await _customerRepository.Create(customer);

            return true;
        }
    }
}
