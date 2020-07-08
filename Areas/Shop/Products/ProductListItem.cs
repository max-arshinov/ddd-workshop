using System;
using System.Linq.Expressions;
using DddWorkshop.Areas.Core.Domain;
using Force.Ddd;

namespace DddWorkshop.Areas.Shop.Products
{
    public class ProductListItem: HasIdBase
    {
        public static readonly Expression<Func<Product, ProductListItem>> Map = x => new ProductListItem
        {
            Id = x.Id,
            Name = x.Name,
            Price = x.Price,
            DiscountPercent = x.DiscountPercent
        };
        
        public string Name { get; set; }
        
        public double Price { get; set; }
        
        public int DiscountPercent { get; set; }

        public override string ToString() => DiscountPercent > 0
            ? $"{Name} ${Price} Sale: ${DiscountPercent}%!"
            : $"{Name} ${Price}";
    }
}