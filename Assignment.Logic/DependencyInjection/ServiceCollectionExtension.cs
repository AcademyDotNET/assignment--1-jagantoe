using Assignment.Logic.Helpers;
using Microsoft.Extensions.DependencyInjection;

namespace Assignment.Logic.DependencyInjection
{
    public static class ServiceCollectionExtension
    {
        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<IPasswordHasher, PasswordHasher>();
            services.AddTransient<IDiscountCalculator, AmountBasedDiscountCalculator>();
            services.AddScoped<IProductService, ProductsService>();
            services.AddScoped<IShopService, ShopService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
