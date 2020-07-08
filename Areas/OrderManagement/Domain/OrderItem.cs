using System;
using System.ComponentModel.DataAnnotations;
using DddWorkshop.Areas.Core.Domain;
using DddWorkshop.Areas.Shop.Domain;

namespace DddWorkshop.Areas.OrderManagement.Domain
{
    public class OrderItem: IntEntityBase
    {
        protected OrderItem()
        {
        }

        internal OrderItem(Order order, CartItem cartItem)
        {
            Order = order;
            Count = cartItem.Count;
            Name = cartItem.ProductName;
            Price = cartItem.Price;
        }
        
        [Required]
        public string Name { get; set; }
        
        public Order Order { get; set; }
        
        public double Price { get; set; }
        
        [Obsolete]
        public int DiscountPercent { get; set; }
        
        public int Count { get; set; }
    }
}