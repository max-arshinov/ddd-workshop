using DddWorkshop.Areas.Core.Infrastructure;
using DddWorkshop.Areas.Shop.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DddWorkshop.Areas.Shop
{
    public class CartController: Controller
    {
        public IActionResult Index() =>
            HttpContext
                .Session
                .Get<Cart>("Cart")
                .PipeTo(View);
    }
}