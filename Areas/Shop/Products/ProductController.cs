using System.Linq;
using DddWorkshop.Areas.Core.Domain;
using DddWorkshop.Areas.Core.Infrastructure;
using DddWorkshop.Areas.Shop.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DddWorkshop.Areas.Shop.Products
{
    public  class  ProductController : Controller
    {
        public IActionResult Index([FromServices] DbContext dbContext /* filters & paging*/) =>
            this.Index(dbContext, ProductListItem.Map, Product.Specs.IsForSale);

        public IActionResult Display([FromServices] DbContext dbContext, int id) =>
            this.Display(dbContext, ProductListItem.Map, id);
        
        [CommitAsync]
        public IActionResult AddToCart(
            [FromServices] DbContext dbContext, 
            [FromServices] CartStorage cartStorage,
            int id)
        {
            var product = dbContext
                .Set<Product>()
                .First(x => x.Id == id);

            cartStorage.Cart.AddProduct(product);
            
       
            this.ShowMessage("Product added");
            return Redirect("/Cart");
        }
    }
}