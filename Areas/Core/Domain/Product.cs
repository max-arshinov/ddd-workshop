using System;
using System.ComponentModel.DataAnnotations;
using DddWorkshop.Areas.Shop.Domain;
using Microsoft.AspNetCore.Mvc;

namespace DddWorkshop.Areas.Core.Domain
{
    public class Product: IntEntityBase
    {
        public static ProductSpecs Specs = new ProductSpecs();
        // EF Only
        protected Product(){}
        
        public Product(string name, double price, int discountPercent)
        {
            // Validation?
            Name = name;
            Price = price;
            DiscountPercent = discountPercent;
        }

        [Required]
        public string Name { get; protected set; }
        
        public double Price { get; protected set; }
        
        public int DiscountPercent { get; protected set; }

        [HiddenInput(DisplayValue = false)]
        public double DiscountedPrice => Price - Price /100 * DiscountPercent;
    }

    public interface IHasDiscount
    {
        int Price { get; set; }
        
        int DiscountPercent { get; set; }

        decimal DiscountedPrice() => Price - Price / 100 * DiscountPercent;
    }
    
}