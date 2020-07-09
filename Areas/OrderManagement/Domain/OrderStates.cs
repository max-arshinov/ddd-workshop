using System;
using System.Threading.Tasks;
using DddWorkshop.Areas.Core.Domain.State;
using DddWorkshop.Areas.OrderManagement.Domain.Pay;

namespace DddWorkshop.Areas.OrderManagement.Domain
{
    public partial class Order
    {
        public abstract class OrderStateBase: SingleStateBase<Order, OrderStatus>
        {
            protected OrderStateBase(Order entity) : base(entity)
            {
            }
        }
        
        public class New: OrderStateBase
        {
            public New(Order entity) : base(entity)
            {
            }

            public override OrderStatus EligibleStatus => OrderStatus.New;

            public async Task<Paid> PayAsync(IPaymentService paymentService)
            {
                await paymentService.PayAsync(Entity.Id);
                return Entity.To<Paid>(OrderStatus.Paid);
            }
        }
        
        public class Paid: OrderStateBase
        {
            public Paid(Order entity) : base(entity)
            {
            }

            public Shipped Ship(Guid trackingCode)
            {
                Entity.TrackingCode = trackingCode;
                return Entity.To<Shipped>(OrderStatus.Shipped);
            }
            
            public override OrderStatus EligibleStatus => OrderStatus.Paid;
        }
        
        public class Shipped: OrderStateBase
        {
            public Shipped(Order entity) : base(entity)
            {
            }
            
            public override OrderStatus EligibleStatus => OrderStatus.Shipped;
        }
        
        public class Complete: OrderStateBase
        {
            public Complete(Order entity) : base(entity)
            {
            }

            public override OrderStatus EligibleStatus => OrderStatus.Complete;
        }
        
        public class Dispute: OrderStateBase
        {
            public Dispute(Order entity) : base(entity)
            {
            }

            public override OrderStatus EligibleStatus => OrderStatus.Dispute;
        }
    }
}