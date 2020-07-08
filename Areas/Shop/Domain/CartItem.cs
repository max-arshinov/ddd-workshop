using System.ComponentModel.DataAnnotations;
using DddWorkshop.Areas.Core.Domain;

namespace DddWorkshop.Areas.Shop.Domain
{
    public class CartItem: IntHasIdBase
    {
        [Required]
        public Product Product { get; set; }
        
        public int Count { get; set; }

        public override string ToString() => Product.ToString();
    }
}