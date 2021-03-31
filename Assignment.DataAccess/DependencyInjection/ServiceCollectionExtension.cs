using Assignment.DataAccess.Repositories;
using Assignment.DataAccess.Repositories.Interfaces;
using Assignment.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment.DataAccess.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static void AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<IRepository<Customer>, CustomerRepository>();
            services.AddScoped<IRepository<Product>, ProductRepository>();
            services.AddScoped<IRepository<ShoppingBag>, ShoppingBagRepository>();
            services.AddScoped<IRepository<ShoppingItem>, ShoppingItemRepository>();
        }
    }
}
