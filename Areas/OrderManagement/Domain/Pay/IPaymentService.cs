using System.Threading.Tasks;

namespace DddWorkshop.Areas.OrderManagement.Domain.Pay
{
    public interface IPaymentService
    {
        Task PayAsync(int orderId);
    }
}