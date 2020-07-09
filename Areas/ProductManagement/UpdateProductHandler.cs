using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using DddWorkshop.Areas.Core.Domain;
using DddWorkshop.Areas.Core.Infrastructure;
using Force.Ccc;
using Force.Cqrs;
using Force.Linq;

namespace DddWorkshop.Areas.ProductManagement
{
    public class UpdateProductHandler: 
        ICommandHandler<UpdateProduct>, 
        IValidator<UpdateProduct>
    {
        private readonly IQueryable<Product> _products;
        private readonly UserContext _userContext;

        private Dictionary<int, Product> _productCache = new Dictionary<int, Product>();
        
        public UpdateProductHandler(IQueryable<Product> products, UserContext userContext)
        {
            _products = products;
            _userContext = userContext;
        }
        
        public Product this[UpdateProduct command]
        {
            get
            {
                // Enforce validation
                if (!_productCache.ContainsKey(command.Id))
                {
                    var res = Validate(command);
                    if (!res.IsValid())
                    {
                        throw new BusinessRuleException("Update command is invalid", res);
                    }
                }
                
                return _productCache[command.Id];
            }
        }
        
        public void Handle(UpdateProduct command)
        {
            var product = this[command];
            product.Update(command, _userContext.User);
        }

        public IEnumerable<ValidationResult> Validate(UpdateProduct command)
        {
            _productCache[command.Id] = _products
                .FirstOrDefaultById(command.Id);

            if (_productCache[command.Id] == null)
            {
                return new []{ new ValidationResult($"Product with id {command.Id} is not found")};
            }
            
            var context = new ValidationContext(command, serviceProvider: null, items: null);
            var results = new List<ValidationResult>();

            Validator.TryValidateObject(command, context, results);
            return results;
        }
    }
}