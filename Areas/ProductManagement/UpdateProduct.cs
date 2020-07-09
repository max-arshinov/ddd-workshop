using System.ComponentModel.DataAnnotations;
using DddWorkshop.Areas.Core.Domain;
using Force.Cqrs;
using Force.Ddd;

namespace DddWorkshop.Areas.ProductManagement
{
    public class UpdateProduct: HasIdBase, ICommand
    {
        // To Consider
        //public Id<Product> Id { get; set; }
        
        [Required, StringLength(255)]
        public string Name { get; protected set; }
        
        [Range(0, 1000000)]
        public double Price { get; protected set; }
        
        [Range(0, 100)]
        public int DiscountPercent { get; protected set; }
    }
}