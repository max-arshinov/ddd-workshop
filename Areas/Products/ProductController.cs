using System.Linq;
using DotNext.DddWorkshop.Areas.Products.Domain;
using DotNext.DddWorkshop.Infrastructure;
using DotNext.DddWorkshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNext.DddWorkshop.Areas.Products
{
    public class ProductController : Controller
    {
        public IActionResult Init([FromServices] DbContext dbContext)
        {
            dbContext.Set<Product>().AddRange(new []
            {
                new Product()
                {
                    Name = "iPhone",
                    Price = 500
                },
                new Product()
                {
                    Name = "MacBook",
                    Price = 1000
                }
            });

            dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult ChangePrice([FromQuery]ChangePrice changePrice)
        {
            return View(changePrice);
        }
        
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

        [HttpPost]
        public IActionResult Edit([FromServices] DbContext dbContext, Product product)
        {
            dbContext.Attach(product);
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