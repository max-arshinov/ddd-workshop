using System;
using System.Collections.Generic;
using System.Linq;
using DddWorkshop.Areas.Core.Domain;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;

namespace DddWorkshop.Areas.Shop.Domain
{
    public sealed class Cart: EntityBase<Guid>
    {
        internal Cart(IdentityUser user)
        {
            User = user;
            Id = Guid.NewGuid();
            _cartItems = new List<CartItem>();
        }

        internal Cart(Guid id, IEnumerable<CartItem> cartItems, IdentityUser user)
        {
            User = user;
            Id = id;
            _cartItems = new List<CartItem>(cartItems);
        }

        public IdentityUser User { get; protected set; }

        private readonly List<CartItem> _cartItems;

        public IEnumerable<CartItem> CartItems => _cartItems;


        public void AddProduct(Product product)
        {
            var ci = _cartItems
                .FirstOrDefault(x => x.ProductId == product.Id);

            if (ci == null)
            {
                ci = new CartItem
                {
                    ProductId = product.Id,
                    Price = product.DiscountedPrice,
                    ProductName = product.Name,
                    Count = 1
                };
                
                _cartItems.Add(ci);
            }
            else
            {
                ci.Count++;
            }
        }

        public bool IsEmpty() => !_cartItems.Any();

        internal CartDto ToDto() => new CartDto
        {
            Id = Id,
            CartItems = _cartItems
        };

        internal static Cart FromDto(CartDto dto, IdentityUser user)
        {
            if (dto == null) return null;
            return new Cart(dto.Id, dto.CartItems, user);
        }
    }
}