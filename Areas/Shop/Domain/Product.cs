using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DddWorkshop.Areas.Shop.Domain
{
    public class Product
    {
        [Key, Required, HiddenInput (DisplayValue = false)] 
        public virtual int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public double Price { get; set; }
        
        public int DiscountPercent { get; set; }

        [HiddenInput(DisplayValue = false)]
        public double DiscountedPrice => Price - Price /100 * DiscountPercent;

        #warning Srp Violation
        public override string ToString() => DiscountPercent > 0
            ? $"{Name} ${Price} Sale: ${DiscountPercent}%!"
            : $"{Name} ${Price}";
    }

    public interface IHasDiscount
    {
        int Price { get; set; }
        
        int DiscountPercent { get; set; }

        decimal DiscountedPrice() => Price - Price / 100 * DiscountPercent;
    }
    
}