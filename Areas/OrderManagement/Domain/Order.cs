using System;
using System.Collections.Generic;
using DddWorkshop.Areas.Core.Domain;
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
    
    #warning No invariant
    public class Order: IntEntityBase
    {
        public static readonly OrderSpecs Specs = new OrderSpecs();
        public IdentityUser User { get; set; }
        
        public DateTime Created { get; set; }
        
        public DateTime Updated { get; set; }

        public OrderState State { get; set; }
        
        public List<OrderItem> OrderItems { get; set; }
        
        public double Total { get; set; }
    }
}