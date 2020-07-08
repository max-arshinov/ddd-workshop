using System;
using System.Collections.Generic;
using DddWorkshop.Areas.Core.Domain;
using Microsoft.AspNetCore.Identity;

namespace DddWorkshop.Areas.Shop.Domain
{
    #warning No invariant
    public class Cart: HasIdBase<Guid>
    {
        public List<CartItem> CartItems { get; set; } = new List<CartItem>();
        
        public IdentityUser User { get; set; }
    }
}