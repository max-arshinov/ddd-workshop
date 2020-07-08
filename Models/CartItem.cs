using System.ComponentModel.DataAnnotations;

namespace DddWorkshop.Models
{
    public class CartItem: IntHasIdBase
    {
        [Required]
        public Product Product { get; set; }
        
        public int Count { get; set; }

        public override string ToString() => Product.ToString();
    }
}