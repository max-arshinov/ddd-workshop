using System;
using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using DddWorkshop.Areas.Core.Domain;
using Force.Cqrs;
using Force.Ddd;
using Microsoft.AspNetCore.Mvc;

namespace DddWorkshop.Areas.ProductManagement
{
    public class UpdateProduct: HasIdBase, ICommand
    {
        [HiddenInput(DisplayValue = false)]
        public override int Id
        {
            get => base.Id; 
            set => base.Id = value;
        }

        public static readonly Expression<Func<Product, UpdateProduct>> Map =
            x => new UpdateProduct
            {
                Id = x.Id,
                Name = x.Name,
                DiscountPercent = x.DiscountPercent,
                Price = x.Price
            };
        
        // To Consider
        //public Id<Product> Id { get; set; }
        
        [Required, StringLength(255)]
        public string Name { get; set; }
        
        [Range(0, 1000000)]
        public double Price { get; set; }
        
        [Range(0, 100)]
        public int DiscountPercent { get; set; }
    }
}