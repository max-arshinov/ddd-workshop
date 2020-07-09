using System.Linq;
using System.Threading.Tasks;
using DddWorkshop.Areas.Core.Infrastructure;
using DddWorkshop.Areas.OrderManagement.Domain;
using DddWorkshop.Areas.OrderManagement.Domain.Dispute;
using DddWorkshop.Areas.OrderManagement.Domain.New;
using DddWorkshop.Areas.OrderManagement.Domain.Pay;
using DddWorkshop.Areas.OrderManagement.Domain.Ship;
using DddWorkshop.Areas.Shop.Domain;
using Force.Ccc;
using Force.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DddWorkshop.Areas.OrderManagement
{
    [Authorize]
    public class OrderController: Controller
    {
        public IActionResult Index([FromServices] DbContext dbContext) => 
            this.Index(dbContext, x => x, Order.Specs.ByIdentity(User.Identity));

        
        [CommitAsync]
        public IActionResult CreateNew(
            [FromServices] DbContext dbContext,
            [FromServices] CartStorage cartStorage)
        {
            var order = new Order(new NewOrder(cartStorage.Cart));
            dbContext.Add(order);
            
            this.ShowMessage("Order created");
            return Redirect("/Order");
        }

        private IActionResult  Match(Result<(OrderState, string), string> result) =>
        result.Match<IActionResult>(x =>
                {
                    this.ShowMessage(x.Item2);
                    return RedirectToAction("Index");
                }, x =>
                {
                    this.ShowError(x);
                    return View();
                });

        [CommitAsync]
        public async Task<IActionResult> Pay(
            [FromServices] PayOrderHandler handler,
            PayOrder command) =>
            (await handler.Handle(command)).PipeTo(Match);


        [CommitAsync]
        public async Task<IActionResult> Ship(
            [FromServices] ShipOrderHandler handler,
            ShipOrder command) =>
            (await handler.Handle(command)).PipeTo(Match);
        
        
        [CommitAsync]
        public async Task<IActionResult> Dispute(
            [FromServices] DisputeOrderHandler handler,
            DisputeOrder command) =>
            (await handler.Handle(command)).PipeTo(Match);
  
        
        // TODO: refactor it
        // Use ActionResut<T> instead?
        public IActionResult Complete(
            [FromServices] DbContext dbContext,
            int id)
        {
            var order = dbContext
                .Set<Order>()
                .First(x => x.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            order.State = OrderState.Complete;
            dbContext.SaveChanges();
            
            this.ShowMessage("Order is complete");
            return RedirectToAction("Index");
        }
    }
}