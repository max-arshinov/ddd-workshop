using System.Threading.Tasks;
using DddWorkshop.Models;

namespace DddWorkshop.Controllers
{
    public class PaymentService
    {
        public Task PayAsync(int orderId)
        {
            return Task.Delay(1000);
        }
    }
}