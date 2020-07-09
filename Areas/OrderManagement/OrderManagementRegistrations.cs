using DddWorkshop.Areas.OrderManagement.Domain;
using Microsoft.Extensions.DependencyInjection;

namespace DddWorkshop.Areas.OrderManagement
{
    public static class OrderManagementRegistration
    {
        public static void RegisterOrderManagement(this IServiceCollection services)
        {
            services.AddScoped<DeliveryService>();
            services.AddScoped<PaymentService>();
            services.AddScoped<DisputeService>();
        }
    }
}