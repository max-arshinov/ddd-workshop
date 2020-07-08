using System;
using System.Linq;
using DddWorkshop.Infrastructure;
using DddWorkshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DddWorkshop.Controllers
{
    public class ProductController : Controller
    {
        public IActionResult Index([FromServices] DbContext dbContext) => 
            dbContext
                .Set<Product>()
                .Where(x => x.Price > 0)
                .ToList()
                .PipeTo(View);

        public IActionResult Display([FromServices] DbContext dbContext, int id) =>
            dbContext
                .Set<Product>()
                .FirstOrDefault(x => x.Id == id)
                .PipeTo(View);
        
        public IActionResult Edit([FromServices] DbContext dbContext, int id) =>
            dbContext
                .Set<Product>()
                .FirstOrDefault(x => x.Id == id)
                .PipeTo(View);

        public IActionResult AddToCart([FromServices] DbContext dbContext, int id)
        {
            var product = dbContext
                .Set<Product>()
                .First(x => x.Id == id);

            var cart = HttpContext
                .Session
                .Get<Cart>("Cart") 
                       ?? new Cart {Id = Guid.NewGuid()};
            
            var ci = cart.CartItems
                .FirstOrDefault(x => x.Product.Id == product.Id);

            if (ci == null)
            {
                ci = new CartItem
                {
                    Product = product,
                    Count = 1
                };
                cart.CartItems.Add(ci);
            }
            else
            {
                ci.Count++;
            }
            
            HttpContext.Session.Set("Cart", cart);
            this.ShowMessage("Product added");
            return Redirect("/Cart");
        }
        
        [HttpPost]
        public IActionResult Edit([FromServices] DbContext dbContext, Product product)
        {
            dbContext.Attach(product);
            dbContext.Update(product);
            #warning Cross-cutting concerns
            dbContext.Add(new AuditLog
            {
                EntityId = product.Id,
                EventName = "Product Updated",
                UserName = User.Identity.IsAuthenticated ? User.Identity.Name : "Anonymous"
            });
            
            dbContext.SaveChanges();
            this.ShowMessage("Product saved");
            return View(product);
        }
    }
}