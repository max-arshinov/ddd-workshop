using System;
using DddWorkshop.Areas.Core.Infrastructure;
using Microsoft.AspNetCore.Http;

namespace DddWorkshop.Areas.Shop.Domain
{
    public class CartStorage
    {
        private readonly IHttpContextAccessor _accessor;

        public CartStorage(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }

        private Cart _cart;
        private static string _cartKey = "Cart";

        public Cart Cart
        {
            get
            {
                return _cart ??= _accessor
                                     .HttpContext
                                     .Session
                                     .Get<Cart>(_cartKey)
                                 ?? new Cart {Id = Guid.NewGuid()};
            }
            set
            {
                _cart = value;
                _accessor
                    .HttpContext
                    .Session
                    .Set(_cartKey, _cart);
            }
        }
    }
}