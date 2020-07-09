using System.Threading.Tasks;
using DddWorkshop.Areas.OrderManagement.Domain.Base;
using Force.Ccc;
using Microsoft.EntityFrameworkCore;

namespace DddWorkshop.Areas.OrderManagement.Domain.Dispute
{
    public class DisputeOrderHandler: ChangeOrderStateHandlerBase<DisputeOrder>
    {
        private readonly DisputeService _disputeService;

        public DisputeOrderHandler(DbContext dbContext, DisputeService disputeService) : base(dbContext)
        {
            _disputeService = disputeService;
        }

        protected override async Task<Result<(OrderStatus, string), string>> DoHandle(Order order, DisputeOrder command)
        {
            // TODO: refactor this
            await _disputeService.DisputeAsync(command.Id);
            //order.Status = OrderStatus.Dispute;
            return (order.Status, "Order is disputed");
        }
    }
}