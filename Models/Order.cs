using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DddWorkshop.Models
{
    public enum OrderState
    {
        New,
        Paid,
        Shipped,
        Complete,
        Dispute
    }
    
    public class Order: IntHasIdBase
    {
        public IdentityUser User { get; set; }
        
        public DateTime Created { get; set; }
        
        public DateTime Updated { get; set; }

        public OrderState State { get; set; }
        
        public List<OrderItem> OrderItems { get; set; }
        
        public double Total { get; set; }
    }
}