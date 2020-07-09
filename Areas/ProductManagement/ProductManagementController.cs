using System;
using System.Linq;
using DddWorkshop.Areas.Core.Domain;
using DddWorkshop.Areas.Core.Infrastructure;
using Force.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace DddWorkshop.Areas.ProductManagement
{
    public class ProductManagementController : Controller
    {
        public IActionResult MassIncreasePrice([FromServices] IQueryable<Product> products)
        {
            products
                .Where(Product.Specs.IsForSale)
                .MassIncreasePrice(new MassIncreasePrice(10));
            
            this.ShowMessage("Prices increased");
            return RedirectToAction("Index", "Product");
        }
        
        [HttpGet("Product/Edit/{id}")]
        public IActionResult Edit([FromServices] IQueryable<Product> products, int id) =>
            products
                .Select(UpdateProduct.Map)
                .FirstOrDefault(x => x.Id == id)
                .PipeTo(View);

        [HttpPost("Product/Edit/{id}")]
        [CommitAsync]
        public IActionResult Edit([FromServices] UpdateProductHandler handler, UpdateProduct command)
        {
            try
            {
                handler.Handle(command);
                this.ShowMessage("Product saved");
                return View(command);
            }
            catch (BusinessRuleException)
            {
                return View(command);
            }
        }
    }
}