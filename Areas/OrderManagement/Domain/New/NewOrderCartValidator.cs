using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DddWorkshop.Areas.Shop.Domain;
using Force.Ccc;

namespace DddWorkshop.Areas.OrderManagement.Domain.New
{
    public class NewOrderCartValidator: IValidator<Cart>
    {
        public IEnumerable<ValidationResult> Validate(Cart cart)
        {
            if(cart == null || cart == null)
                yield return new ValidationResult("Cart is required", new[]{"Cart"});
         
            if(cart?.User == null)
                yield return new ValidationResult("Cart must have a User", new[]{"Cart"});
        }
    }
}