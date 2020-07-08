using DddWorkshop.Infrastructure;
using DddWorkshop.Models;
using Microsoft.AspNetCore.Mvc;

namespace DddWorkshop.Controllers
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