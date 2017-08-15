using Microsoft.AspNetCore.Mvc.RazorPages;
using PoGo.Web.Dto;
using PoGo.Web.Logic;
using System.Collections.Generic;
using System.Linq;

namespace PoGo.Web.Pages
{
    public class IndexModel : PageModel
    {
        private readonly CarouselFeed carouselFeed;
        private readonly MapFeed mapFeed;

        public IList<string> Images { get; set; }
        public IEnumerable<MapInfoDto> Maps { get; set; }

        public IndexModel(CarouselFeed carouselFeed, MapFeed mapFeed)
        {
            this.carouselFeed = carouselFeed;
            this.mapFeed = mapFeed;
        }

        public void OnGet()
        {
            Images = carouselFeed.Images;
            Maps = mapFeed.Maps
                .Take(3)
                .ToList();
        }
    }
}
