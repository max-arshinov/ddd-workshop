using System.Linq;
using DddWorkshop.Areas.AdminArea.Domain;
using DddWorkshop.Areas.Core.Domain;
using DddWorkshop.Areas.Core.Infrastructure;
using DddWorkshop.Areas.Shop.Domain;
using Force.Extensions;
using Force.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DddWorkshop.Areas.ProductManagement
{
    public class ProductManagementController : Controller
    {
        [Route("Product/Edit/{id}")]
        public IActionResult Edit([FromServices] DbContext dbContext, int id) =>
            dbContext
                .Set<Product>()
                .FirstOrDefault(x => x.Id == id)
                .PipeTo(View);
        
        
        [HttpPost]
        [Route("Product/Edit/{id}")]
        [CommitAsync]
        public IActionResult Edit([FromServices] DbContext dbContext, UpdateProduct command)
        {
            var product = dbContext
                .Set<Product>()
                .FirstOrDefaultById(command.Id);

            if (product == null)
            {
                return NotFound();
            }
            
            dbContext.Attach(product);
            dbContext.Update(product);
            dbContext.Add(new AuditLog
            {
                EntityId = product.Id,
                EventName = "Product Updated",
                UserName = User.Identity.IsAuthenticated ? User.Identity.Name : "Anonymous"
            });
            
            this.ShowMessage("Product saved");
            return View(product);
        }
    }
}