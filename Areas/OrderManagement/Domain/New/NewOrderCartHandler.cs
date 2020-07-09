using DddWorkshop.Areas.Core.Infrastructure;
using DddWorkshop.Areas.Shop.Domain;
using Force.Ccc;
using Force.Cqrs;

namespace DddWorkshop.Areas.OrderManagement.Domain.New
{
    public class NewOrderHandler: ICommandHandler<NewOrder, int>
    {
        private readonly IValidator<Cart> _validator;
        private readonly ICartStorage _cartStorage;
        private readonly IUnitOfWork _uow;

        public NewOrderHandler(/* can be moved to decorators*/ IValidator<Cart> validator, 
            ICartStorage cartStorage, IUnitOfWork uow)
        {
            _validator = validator;
            _cartStorage = cartStorage;
            _uow = uow;
        }

        public int Handle(NewOrder command)
        {
            var vaidationResults = _validator.Validate(_cartStorage.Cart);
            if (!vaidationResults.IsValid())
            {
                throw new BusinessRuleException("Can't create a new order", vaidationResults);
            }
            
            var order = new Order(_cartStorage.Cart);
            _uow.Add(order);
            _uow.Commit();
            return order.Id;
        }
    }
}