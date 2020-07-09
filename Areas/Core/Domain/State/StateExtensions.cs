using System;
using Microsoft.AspNetCore.Mvc;

namespace DddWorkshop.Areas.Core.Domain.State
{
    public static class StateExtensions
    {
        public static ActionResult MatchState<TStatus, TBaseState, TConcreteState>(
            this HasStateBase<TStatus, TBaseState> stateBase,
            Func<TConcreteState, object> func) 
            where TStatus : Enum
            where TBaseState: class
            where TConcreteState: class
            => Match(stateBase?.State, func);

        public static ActionResult Match<TState>(
            this object state,
            Func<TState, object> func)
        {
            if (state == null)
            {
                return new NotFoundResult();
            }
            
            if(state is TState state2)
            {
                return new OkObjectResult(func(state2));
            }
            else
            {
                return new BadRequestResult();
            }
        }
    }
}