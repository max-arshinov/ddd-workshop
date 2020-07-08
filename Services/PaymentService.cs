using System.Threading.Tasks;

namespace DddWorkshop.Services
{
    public class PaymentService
    {
        public Task PayAsync(int orderId)
        {
            return Task.Delay(1000);
        }
    }
}