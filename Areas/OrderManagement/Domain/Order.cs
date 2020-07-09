using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DddWorkshop.Areas.Core.Domain;
using DddWorkshop.Areas.Core.Domain.State;
using DddWorkshop.Areas.OrderManagement.Domain.New;
using DddWorkshop.Areas.Shop.Domain;
using Microsoft.AspNetCore.Identity;

namespace DddWorkshop.Areas.OrderManagement.Domain
{
    public partial class Order: 
        HasStateBase<OrderStatus, Order.OrderStateBase> 
    {
        public static readonly OrderSpecs Specs = new OrderSpecs();

        protected Order()
        {
        }

        internal Order(Cart cart)
        {
            if (cart == null) throw new ArgumentNullException(nameof(cart));
            User = cart.User ?? throw new InvalidOperationException("User must be authenticated");
            
            _orderItems = cart
                .CartItems
                .Select(x => new OrderItem(this, x)
                {
                    Order = this,
                    Count = x.Count,
                    Name = x.ProductName,
                    Price = x.Price
                })
                .ToList();

            Total = _orderItems.Select(x => x.Price).Sum();
            Status = OrderStatus.New;
        }
        
        public override OrderStateBase GetState(OrderStatus status) =>
            status switch
            {
                OrderStatus.New => new New(this),
                OrderStatus.Paid => new Paid(this),
                OrderStatus.Shipped => new Shipped(this),
                OrderStatus.Dispute => new Dispute(this),
                OrderStatus.Complete => new Complete(this),
                //https://github.com/dotnet/csharplang/issues/2266
                //see also https://github.com/ardalis/SmartEnum
                _ => throw new NotSupportedException($"Status \"{status}\" is not supported")
            };

        [Required]
        public IdentityUser User { get; protected set; }

        public DateTime Created { get; protected set; } = DateTime.UtcNow;
        
        public DateTime Updated { get; protected set; }
        
        private List<OrderItem> _orderItems = new List<OrderItem>();
        public IEnumerable<OrderItem> OrderItems => _orderItems;
        
        public double Total { get; protected set; }
        
        public Guid? TrackingCode { get; protected set; }
    }
}