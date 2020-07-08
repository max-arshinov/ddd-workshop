using System;
using System.Collections.Generic;
using System.Linq;
using DddWorkshop.Areas.Core.Domain;
using DddWorkshop.Areas.Shop.Domain;
using Microsoft.AspNetCore.Identity;

namespace DddWorkshop.Areas.OrderManagement.Domain
{
    public enum OrderState
    {
        New,
        Paid,
        Shipped,
        Complete,
        Dispute
    }
    
    public class Order: IntEntityBase
    {
        public static readonly OrderSpecs Specs = new OrderSpecs();

        protected Order()
        {
        }

        public Order(Cart cart)
        {
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
            State = OrderState.New;
        }

        public IdentityUser User { get; protected set; }
        
        public DateTime Created { get; protected set; }
        
        public DateTime Updated { get; protected set; }

        public OrderState State { get; set; }

        private List<OrderItem> _orderItems = new List<OrderItem>();
        public IEnumerable<OrderItem> OrderItems => _orderItems;
        
        public double Total { get; protected set; }
    }
}