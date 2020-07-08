using DddWorkshop.Areas.Core.Infrastructure;
using DddWorkshop.Areas.Shop.Domain;
using Force.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DddWorkshop.Areas.Shop
{
    public class CartController: Controller
    {
        public IActionResult Index([FromServices] CartStorage storage) =>
            storage
                .Cart
                .PipeTo(View);
    }
}