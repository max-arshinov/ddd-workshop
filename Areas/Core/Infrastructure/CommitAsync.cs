using System;
using System.Threading.Tasks;
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
            var dbc = (DbContext)context.HttpContext.RequestServices.GetService(typeof(DbContext));
            await dbc.SaveChangesAsync();
        }
    }
}