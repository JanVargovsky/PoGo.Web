using Microsoft.AspNetCore.Mvc.RazorPages;
using System;

namespace PoGo.Web.Pages
{
    public class VersionModel : PageModel
    {
        private readonly static DateTime applicationStart;

        public DateTime ApplicationStart { get; set; }
        public DateTime CurrentDate { get; set; }
        public TimeSpan TotalTime { get; set; }

        static VersionModel()
        {
            applicationStart = DateTime.Now;
        }

        public void OnGet()
        {
            ApplicationStart = applicationStart;
            CurrentDate = DateTime.Now;
            TotalTime = CurrentDate - ApplicationStart;
        }
    }
}