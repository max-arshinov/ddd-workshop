using System.Threading.Tasks;

namespace DddWorkshop.Areas.OrderManagement.Domain.Pay
{
    public class PaymentService : IPaymentService
    {
        public Task PayAsync(int orderId)
        {
            return Task.Delay(1000);
        }
    }
}