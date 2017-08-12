using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace PoGo.Web.Pages
{
    public class CarouselModel : PageModel
    {
        public IList<string> Images { get; set; }

        public CarouselModel()
        {

        }

        public CarouselModel(IList<string> images)
        {
            Images = images;
        }
    }
}