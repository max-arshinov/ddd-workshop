using System;
using DddWorkshop.Areas.Shop.Domain;
using Force.Cqrs;

namespace DddWorkshop.Areas.OrderManagement.Domain.New
{
    public class NewOrder: ICommand
    {
        public NewOrder(Cart cart)
        {
            Cart = cart ?? throw new ArgumentNullException(nameof(cart));
        }

        public Cart Cart { get; }
    }
}