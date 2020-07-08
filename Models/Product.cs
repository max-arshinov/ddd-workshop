using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DddWorkshop.Models
{
    public class Product
    {
        [Key, Required, HiddenInput (DisplayValue = false)] 
        public virtual int Id { get; set; }
        
        [Required]
        public string Name { get; set; }
        
        public int Price { get; set; }
        
        public int DiscountPercent { get; set; }

        [HiddenInput(DisplayValue = false)]
        public decimal DiscountedPrice => Price - Price /100 * DiscountPercent;

        public override string ToString() => $"{Name} ${Price}";
    }

    public interface IHasDiscount
    {
        int Price { get; set; }
        
        int DiscountPercent { get; set; }

        decimal DiscountedPrice() => Price - Price / 100 * DiscountPercent;
    }
    
}