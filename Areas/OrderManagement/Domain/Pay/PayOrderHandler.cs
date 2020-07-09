using System;
using System.Threading.Tasks;
using DddWorkshop.Areas.OrderManagement.Domain.Base;
using Force.Ccc;
using Microsoft.EntityFrameworkCore;

namespace DddWorkshop.Areas.OrderManagement.Domain.Pay
{
    public class PayOrderHandler: ChangeOrderStateHandlerBase<PayOrder>
    {
        private readonly PaymentService _paymentService;

        public PayOrderHandler(DbContext dbContext, PaymentService paymentService) : base(dbContext)
        {
            _paymentService = paymentService;
        }

        protected override async Task<Result<(OrderState, string), string>> DoHandle(Order order, PayOrder command)
        {
            #warning Distributed transaction. No error handling
            await _paymentService.PayAsync(command.Id);

            #warning Encapsulation
            order.State = OrderState.Paid;
            return (order.State, "Order is paid");
        }
    }
}