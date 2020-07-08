using System.Linq;
using DotNext.DddWorkshop.Areas.Products.Domain;
using DotNext.DddWorkshop.Infrastructure;
using DotNext.DddWorkshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DotNext.DddWorkshop.Areas.AudtiLogs
{
    public class AuditLogController : Controller
    {
        public IActionResult Index([FromServices] DbContext dbContext) => 
            dbContext
                .Set<AuditLog>()
                .ToList()
                .PipeTo(View);
        
        public IActionResult Display([FromServices] DbContext dbContext, int id) =>
            dbContext
                .Set<AuditLog>()
                .FirstOrDefault(x => x.Id == id)
                .PipeTo(View);
    }
}