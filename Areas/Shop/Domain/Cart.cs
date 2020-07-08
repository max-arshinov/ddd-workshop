using System;
using System.Collections.Generic;
using System.Linq;
using DddWorkshop.Areas.Core.Domain;
using Newtonsoft.Json;

namespace DddWorkshop.Areas.Shop.Domain
{
    public sealed class Cart: EntityBase<Guid>
    {
        internal Cart()
        {
            Id = Guid.NewGuid();
            _cartItems = new List<CartItem>();
        }

        internal Cart(Guid id, IEnumerable<CartItem> cartItems)
        {
            Id = id;
            _cartItems = new List<CartItem>(cartItems);
        }
        
        private List<CartItem> _cartItems;

        public IEnumerable<CartItem> CartItems => _cartItems;


        public void AddProduct(Product product)
        {
            var ci = _cartItems
                .FirstOrDefault(x => x.Product.Id == product.Id);

            if (ci == null)
            {
                ci = new CartItem
                {
                    Product = product,
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

        internal static Cart FromDto(CartDto dto)
        {
            if (dto == null) return null;
            return new Cart(dto.Id, dto.CartItems);
        }
    }
}