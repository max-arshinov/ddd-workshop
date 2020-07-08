using System.Threading.Tasks;

namespace DddWorkshop.Services
{
    public class DeliveryService
    {
        public Task ShipAsync(int orderId)
        {
            return Task.Delay(500);
        }
    }
}