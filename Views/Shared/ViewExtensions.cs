using Microsoft.AspNetCore.Mvc.Rendering;

namespace DddWorkshop.Views.Shared
{
    public static class ViewExtensions
    {
        public static bool HasMessage(this IHtmlHelper html) => 
            html.TempData["Message"] is string;

        public static string GetMessage(this IHtmlHelper html) =>
            html.TempData["Message"] as string;
    }
}