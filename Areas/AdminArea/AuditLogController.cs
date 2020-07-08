using System.Linq;
using DddWorkshop.Areas.AdminArea.Domain;
using DddWorkshop.Areas.Core.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DddWorkshop.Areas.AdminArea
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