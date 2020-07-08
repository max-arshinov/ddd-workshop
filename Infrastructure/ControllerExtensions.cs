using Microsoft.AspNetCore.Mvc;

namespace DddWorkshop.Infrastructure
{
    public static class ControllerExtensions
    {
        public static void ShowMessage(this Controller c, string message)
        {
            c.TempData["Message"] = message;
        }
    }
}