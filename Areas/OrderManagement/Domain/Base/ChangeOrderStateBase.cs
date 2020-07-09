using System.Threading.Tasks;
using Force.Ccc;
using Force.Cqrs;
using Force.Ddd;
using Microsoft.AspNetCore.Mvc;

namespace DddWorkshop.Areas.OrderManagement.Domain.Base
{
    public abstract class ChangeOrderStateBase: 
        IHasId<int>,
        ICommand<Task<Result<(OrderState, string), string>>>
    {
        object IHasId.Id => Id;

        public int Id { get; set; }
    }
}