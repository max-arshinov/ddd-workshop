using System.Threading.Tasks;

namespace DddWorkshop.Services
{
    public class DisputeService
    {
        public Task DisputeAsync(int orderId)
        {
            return Task.Delay(2000);
        }
    }
}