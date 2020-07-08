using System.ComponentModel.DataAnnotations;

namespace DddWorkshop.Models
{
    public class OrderItem: IntHasIdBase
    {
        [Required]
        public string Name { get; set; }
        
        public Order Order { get; set; }
        
        public double Price { get; set; }
        
        public int DiscountPercent { get; set; }
        
        public int Count { get; set; }
    }
}