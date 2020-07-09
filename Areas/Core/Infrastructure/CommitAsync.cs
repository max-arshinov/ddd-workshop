using System;
using System.Threading.Tasks;
using DddWorkshop.Areas.Shop.Domain;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.EntityFrameworkCore;

namespace DddWorkshop.Areas.Core.Infrastructure
{
    public class CommitAsync : Attribute, IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var task = await next();
            if (task.Exception != null)
            {
                return;
            }
            
            var cartStorage = (ICartStorage)context.HttpContext.RequestServices.GetService(typeof(ICartStorage));
            cartStorage.SaveChanges();
            
            var dbc = (DbContext)context.HttpContext.RequestServices.GetService(typeof(DbContext));
            await dbc.SaveChangesAsync();
            // TODO: notifications
        }
    }
}