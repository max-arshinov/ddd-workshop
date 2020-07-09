using System.Threading.Tasks;

namespace DddWorkshop.Areas.OrderManagement.Domain.Dispute
{
    public class DisputeService
    {
        public Task DisputeAsync(int orderId)
        {
            return Task.Delay(2000);
        }
    }
}