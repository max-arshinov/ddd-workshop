using System.Threading.Tasks;

namespace DddWorkshop.Areas.OrderManagement.Domain
{
    public class DeliveryService
    {
        public Task ShipAsync(int orderId)
        {
            return Task.Delay(500);
        }
    }
}