using System;
using System.Collections.Generic;

namespace DddWorkshop.Areas.Shop.Domain
{
    public class CartDto
    {
        public Guid Id { get; set; }

        public List<CartItem> CartItems { get; set; }
    }
}