using System.Threading.Tasks;
using DddWorkshop.Areas.OrderManagement.Domain.Base;
using DddWorkshop.Areas.OrderManagement.Domain.Pay;
using Force.Ccc;
using Microsoft.EntityFrameworkCore;

namespace DddWorkshop.Areas.OrderManagement.Domain.Ship
{
    public class ShipOrderHandler: ChangeOrderStateHandlerBase<ShipOrder>
    {
        private readonly DeliveryService _deliveryService;

        public ShipOrderHandler(DbContext dbContext, DeliveryService deliveryService) : base(dbContext)
        {
            _deliveryService = deliveryService;
        }

        protected override async Task<Result<(OrderState, string), string>> DoHandle(Order order, ShipOrder command)
        {
            await _deliveryService.ShipAsync(command.Id);
            order.State = OrderState.Shipped;
            
            return (order.State, "Order is shipped");
        }
    }
}