using System.Threading.Tasks;

namespace DddWorkshop.Areas.OrderManagement.Domain
{
    public class DisputeService
    {
        public Task DisputeAsync(int orderId)
        {
            return Task.Delay(2000);
        }
    }
}