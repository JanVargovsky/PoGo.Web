using Microsoft.AspNetCore.Mvc.RazorPages;
using PoGo.Web.Dto;
using PoGo.Web.Logic;
using System.Collections.Generic;

namespace PoGo.Web.Pages
{
    public class MapsModel : PageModel
    {
        private readonly MapFeed mapFeed;

        public IList<MapInfoDto> Maps { get; set; }

        public MapsModel(MapFeed mapFeed)
        {
            this.mapFeed = mapFeed;
        }

        public void OnGet()
        {
            Maps = mapFeed.Maps;
        }
    }
}