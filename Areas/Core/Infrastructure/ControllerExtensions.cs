using Microsoft.AspNetCore.Mvc;

namespace DddWorkshop.Areas.Core.Infrastructure
{
    public static class ControllerExtensions
    {
        public static void ShowMessage(this Controller c, string message)
        {
            c.TempData["Message"] = message;
        }
    }
}