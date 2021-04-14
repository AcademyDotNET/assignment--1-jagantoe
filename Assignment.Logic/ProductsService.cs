using Assignment.DataAccess.Repositories.Interfaces;
using Assignment.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assignment.Logic
{
    public interface IProductService
    {
        Task<Product> GetProduct(int id);
        Task<IEnumerable<Product>> GetProductsList(int page);
        Task CreateProduct(string productName, int productPrice);
        Task UpdateProduct(int productId, string productName, int productPrice);
        Task DeleteProduct(int productId);
    }

    public class ProductsService : IProductService
    {
        private readonly IRepository<Product> _productRepository;

        public ProductsService(IRepository<Product> productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Product> GetProduct(int id)
        {
            return await _productRepository.Single(_ => _.Id == id);
        }

        public async Task<IEnumerable<Product>> GetProductsList(int page)
        {
            var products = await _productRepository.All();

            var max = (int)Math.Ceiling(products.Count() / 9.0);

            //Refactor
            page = page < 0 ? 0 : page > max ? max : page;

            products = products.Skip((page - 1) * 9).Take(9).ToList();

            return products;
        }

        public async Task CreateProduct(string productName, int productPrice)
        {
            var product = new Product(productName, productPrice, "");

            await _productRepository.Create(product);
        }

        public async Task UpdateProduct(int productId, string productName, int productPrice)
        {
            var product = await _productRepository.Single(_ => _.Id == productId);

            product.SetName(productName);

            product.SetPrice(productPrice);

            await _productRepository.Update(product);
        }

        public async Task DeleteProduct(int productId)
        {
            var product = await _productRepository.Single(_ => _.Id == productId);

            if (product != null)
            {
                await _productRepository.Delete(product);
            }
        }
    }
}
