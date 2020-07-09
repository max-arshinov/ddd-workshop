using System;
using System.Linq;
using System.Linq.Expressions;
using DddWorkshop.Areas.Core.Domain;
using DddWorkshop.Areas.Shop.Products;
using Force.Ddd;
using Force.Extensions;
using Force.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DddWorkshop.Areas.Core.Infrastructure
{
    public static class ControllerExtensions
    {
        public static void ShowMessage(this Controller c, string message)
        {
            c.TempData["Message"] = message;
        }
        
        public static void ShowError(this Controller c, string message)
        {
            c.TempData["Error"] = message;
        }
       
        public static IActionResult Display<TEntity, TDto>(
            this Controller controller,
            [FromServices] DbContext dbContext,
            Expression<Func<TEntity, TDto>> map,
            int id)
            where TEntity : class
            where TDto : class, IHasId<int> =>
            dbContext
                .Set<TEntity>()
                .Select(map)
                .FirstOrDefaultById(id)
                .PipeTo(controller.View);

        public static IActionResult Index<TEntity, TDto>(
            this Controller controller,
            [FromServices] DbContext dbContext,
            Expression<Func<TEntity, TDto>> map,
            Spec<TEntity> spec = null)
            where TEntity : class
            where TDto : class, IHasId<int> =>
            dbContext
                .Set<TEntity>()
                .WhereIfNotNull(spec, spec)
                // No lazy load, no change tracking
                // AutoMapper?
                .Select(map)
                // AutoFilters
                .ToList()
                .PipeTo(controller.View);

    }
}