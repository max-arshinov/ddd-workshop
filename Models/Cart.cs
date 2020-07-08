using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

namespace DddWorkshop.Models
{
    public class Cart: HasIdBase<Guid>
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        
        public IdentityUser User { get; set; }
    }
}