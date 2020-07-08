using System.Linq;
using DddWorkshop.Areas.AdminArea.Domain;
using DddWorkshop.Areas.Core.Infrastructure;
using DddWorkshop.Areas.Shop.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DddWorkshop.Areas.ProductManagement
{
    public partial class  ProductController : Controller
    {
        public IActionResult Edit([FromServices] DbContext dbContext, int id) =>
            dbContext
                .Set<Product>()
                .FirstOrDefault(x => x.Id == id)
                .PipeTo(View);
        
        
        [HttpPost]
        public IActionResult Edit([FromServices] DbContext dbContext, Product product)
        {
            dbContext.Attach(product);
            dbContext.Update(product);
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