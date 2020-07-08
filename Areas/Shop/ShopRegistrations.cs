using DddWorkshop.Areas.Shop.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace DddWorkshop.Areas.Shop
{
    public static class ShopRegistrations
    {
        public static void RegisterShop(this IServiceCollection services)
        {
            services.AddScoped<CartStorage>();
        }
    }
}