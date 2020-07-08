using System.Linq;
using DddWorkshop.Infrastructure;
using DddWorkshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DddWorkshop.Controllers
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