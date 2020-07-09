using System;
using System.Threading.Tasks;
using DddWorkshop.Areas.OrderManagement.Domain.Base;
using Force.Ccc;
using Microsoft.EntityFrameworkCore;

namespace DddWorkshop.Areas.OrderManagement.Domain.Pay
{
    public class PayOrderHandler: ChangeOrderStateHandlerBase<PayOrder>
    {
        private readonly IPaymentService _paymentService;

        public PayOrderHandler(DbContext dbContext, IPaymentService paymentService) : base(dbContext)
        {
            _paymentService = paymentService;
        }

        protected override async Task<Result<(OrderStatus, string), string>> DoHandle(Order order, PayOrder command)
        {
            if(order.State is Order.New newOrder)
            {
                // TODO: order calculator
                await newOrder.PayAsync(_paymentService);
                return (order.Status, "Order is paid");
            }
            
            return "Order is in wrong state";
        }
    }
}