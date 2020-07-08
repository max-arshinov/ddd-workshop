using System.Threading.Tasks;

namespace DddWorkshop.Areas.OrderManagement.Domain
{
    public class PaymentService
    {
        public Task PayAsync(int orderId)
        {
            return Task.Delay(1000);
        }
    }
}