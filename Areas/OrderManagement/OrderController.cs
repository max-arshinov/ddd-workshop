using System.Linq;
using System.Threading.Tasks;
using DddWorkshop.Areas.Core.Infrastructure;
using DddWorkshop.Areas.OrderManagement.Domain;
using DddWorkshop.Areas.Shop.Domain;
using Force.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DddWorkshop.Areas.OrderManagement
{
    [Authorize]
    public class OrderController: Controller
    {
        public IActionResult Index([FromServices] DbContext dbContext) => 
            dbContext
                #warning Include or Lazy load might be required in the future
                .Set<Order>()
                .Where(x => x.User.UserName == User.Identity.Name)
                .ToList()
                .PipeTo(View);

        [Authorize]
        [CommitAsync]
        public IActionResult CreateNew([FromServices] DbContext dbContext)
        {
            #warning Encapsulation
            var cart = HttpContext.Session.Get<Cart>("Cart");
            var order = new Order
            {
                User = dbContext
                    .Set<IdentityUser>()
                    .First(x => x.UserName == User.Identity.Name)
            };

            order.OrderItems = cart
                .CartItems
                .Select(x => new OrderItem
                {
                    Order = order,
                    Count = x.Count,
                    Name = x.Product.Name,
                    Price = x.Product.Price,
                    DiscountPercent = x.Product.DiscountPercent
                })
                .ToList();

            order.Total = order.OrderItems.Select(x => x.Price - x.Price / 100 * x.DiscountPercent).Sum();
            dbContext.Add(order);
            dbContext.SaveChanges();
            
            this.ShowMessage("Order created");
            return Redirect("/Order");
        }

        #warning DRY violation
        public async Task<IActionResult> Pay(
            [FromServices] DbContext dbContext, 
            [FromServices] PaymentService paymentService, 
            int id)
        {
            var order = dbContext
                .Set<Order>()
                .First(x => x.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            #warning Distributed transaction. No error handling
            await paymentService.PayAsync(id);
            
            #warning Encapsulation
            order.State = OrderState.Paid;
            dbContext.SaveChanges();
            
            this.ShowMessage("Order is paid");
            return Redirect("../");
        }
        
        #warning DRY violation
        public async Task<IActionResult> Ship(
            [FromServices] DbContext dbContext, 
            [FromServices] DeliveryService deliveryService, 
            int id)
        {
            var order = dbContext
                .Set<Order>()
                .First(x => x.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            await deliveryService.ShipAsync(id);
            order.State = OrderState.Shipped;
            dbContext.SaveChanges();
            
            this.ShowMessage("Order is shipped");
            return RedirectToAction("Index");
        }
        
        #warning DRY violation
        public async Task<IActionResult> Dispute(
            [FromServices] DbContext dbContext,
            [FromServices] DisputeService disputeService, 
            int id)
        {
            var order = dbContext
                .Set<Order>()
                .First(x => x.Id == id);

            if (order == null)
            {
                return NotFound();
            }

            await disputeService.DisputeAsync(id);
            order.State = OrderState.Dispute;
            dbContext.SaveChanges();
            
            this.ShowMessage("Order is disputed");
            return RedirectToAction("Index");
        }
        
        #warning DRY violation
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