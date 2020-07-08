using System;
using System.Collections.Generic;
using DddWorkshop.Areas.Core.Infrastructure;
using Force.Extensions;
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

        public Cart Cart =>
            _cart ??= _accessor
                          .HttpContext
                          .Session
                          .Get<CartDto>(_cartKey)
                          .PipeTo(Cart.FromDto)
                      ?? new Cart();

        public void SaveChanges()
        {
            _accessor
                .HttpContext
                .Session
                .Set(_cartKey, _cart.ToDto());
        }
    }
}