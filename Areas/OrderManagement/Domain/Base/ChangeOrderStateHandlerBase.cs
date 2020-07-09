using System.Threading.Tasks;
using Force.Ccc;
using Force.Cqrs;
using Force.Ddd;
using Microsoft.EntityFrameworkCore;

namespace DddWorkshop.Areas.OrderManagement.Domain.Base
{
    public abstract class ChangeOrderStateHandlerBase<T>: 
        ICommandHandler<T, Task<Result<(OrderStatus, string), string>>>
        where T: IHasId<int>, ICommand<Task<Result<(OrderStatus, string), string>>>
    {
        private readonly DbContext _dbContext;

        protected ChangeOrderStateHandlerBase(DbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<Result<(OrderStatus, string), string>> Handle(T command)
        {
            var orderResult = await GetOrder(command);
            return await orderResult.Match(
                x => DoHandle(x, command), 
                x => Task.FromResult(new Result<(OrderStatus, string), string>(x)));
        }

        protected abstract Task<Result<(OrderStatus, string), string>> DoHandle(Order order, T command);

        private async Task<Result<Order, string>> GetOrder(T command)
        {
            var order = await _dbContext
                .Set<Order>()
                .FirstOrDefaultAsync(x => x.Id == command.Id);

            return order != null
                ? new Result<Order, string>(order)
                : "Order not found";
        }
    }
}