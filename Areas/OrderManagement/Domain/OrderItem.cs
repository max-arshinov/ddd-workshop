using System;
using System.ComponentModel.DataAnnotations;
using DddWorkshop.Areas.Core.Domain;

namespace DddWorkshop.Areas.OrderManagement.Domain
{
    public class OrderItem: IntHasIdBase
    {
        [Required]
        public string Name { get; set; }
        
        public Order Order { get; set; }
        
        public double Price { get; set; }
        
        [Obsolete]
        public int DiscountPercent { get; set; }
        
        public int Count { get; set; }
    }
}