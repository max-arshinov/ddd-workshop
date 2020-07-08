using System;
using System.Collections.Generic;
using DddWorkshop.Areas.Core.Domain;
using DddWorkshop.Areas.Core.Infrastructure;
using Force.Extensions;
using Microsoft.AspNetCore.Http;

namespace DddWorkshop.Areas.Shop.Domain
{
    public class CartStorage
    {
        private readonly IHttpContextAccessor _accessor;
        private readonly UserContext _userContext;

        public CartStorage(IHttpContextAccessor accessor, UserContext userContext)
        {
            _accessor = accessor;
            _userContext = userContext;
        }

        private Cart _cart;
        private static string _cartKey = "Cart";

        public Cart Cart =>
            _cart ??= _accessor
                          .HttpContext
                          .Session
                          .Get<CartDto>(_cartKey)
                          .PipeTo(x => Cart.FromDto(x, _userContext.User))
                      ?? new Cart(_userContext.User);

        public void SaveChanges()
        {
            if (_cart != null)
            {
                _accessor
                    .HttpContext
                    .Session
                    .Set(_cartKey, _cart.ToDto());
            }
        }
    }
}