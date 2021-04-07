using Assignment.DataAccess.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using MVC_Assignment.Logic.Helpers;

namespace Assignment.Logic.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddScoped<IProductService, ProductsService>();
            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
