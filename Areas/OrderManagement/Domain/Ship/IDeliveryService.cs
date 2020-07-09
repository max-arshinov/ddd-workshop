using System;
using System.Threading.Tasks;

namespace DddWorkshop.Areas.OrderManagement.Domain.Ship
{
    public class IDeliveryService
    {
        public Task<Guid> ShipAsync(int orderId)
        {
            return Task.FromResult(Guid.NewGuid());
        }
    }
}