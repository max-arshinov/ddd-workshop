using DddWorkshop.Areas.OrderManagement.Domain;
using DddWorkshop.Areas.OrderManagement.Domain.Dispute;
using DddWorkshop.Areas.OrderManagement.Domain.Pay;
using DddWorkshop.Areas.OrderManagement.Domain.Ship;
using Microsoft.Extensions.DependencyInjection;

namespace DddWorkshop.Areas.OrderManagement
{
    public static class OrderManagementRegistration
    {
        public static void RegisterOrderManagement(this IServiceCollection services)
        {
            services.AddScoped<IDeliveryService, DeliveryService>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<DisputeService>();

            services.AddScoped<PayOrderHandler>();
            services.AddScoped<ShipOrderHandler>();
            services.AddScoped<DisputeOrderHandler>();
        }
    }
}