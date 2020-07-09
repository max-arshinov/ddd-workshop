using System.Threading.Tasks;
using DddWorkshop.Areas.OrderManagement.Domain.Base;
using DddWorkshop.Areas.OrderManagement.Domain.Pay;
using Force.Ccc;
using Microsoft.EntityFrameworkCore;

namespace DddWorkshop.Areas.OrderManagement.Domain.Ship
{
    public class ShipOrderHandler: ChangeOrderStateHandlerBase<ShipOrder>
    {
        private readonly IDeliveryService _deliveryService;

        public ShipOrderHandler(DbContext dbContext, IDeliveryService deliveryService) : base(dbContext)
        {
            _deliveryService = deliveryService;
        }

        protected override async Task<Result<(OrderStatus, string), string>> DoHandle(Order order, ShipOrder command)
        {
            if(order.State is Order.Paid newOrder)
            {
                // TODO: error handling and compensate actions here
                var trackingCode = await _deliveryService.ShipAsync(command.Id);
                newOrder.Ship(trackingCode);
                return (order.Status, "Order is shipped");
            }
            return "Order is in wrong state";
        }
    }
}