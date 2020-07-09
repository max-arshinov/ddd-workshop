using System.Linq;
using DddWorkshop.Areas.Core.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DddWorkshop.Areas.ProductManagement
{
    public static class ProductManagementRegistrations
    {
        public static void RegisterProductManagement(this IServiceCollection services)
        {
            services.AddScoped<IQueryable<Product>>(x => x
                .GetService<DbContext>()
                .Set<Product>()
            );
            
            services.AddScoped<UpdateProductHandler>();
        }
    }
}