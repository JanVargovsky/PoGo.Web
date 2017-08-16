using Microsoft.AspNetCore.Mvc;
using PoGo.Web.Logic;
using System.Linq;

namespace PoGo.Web.Controllers
{
    public class MapController : Controller
    {
        private readonly MapFeed mapFeed;

        public MapController(MapFeed mapFeed)
        {
            this.mapFeed = mapFeed;
        }

        [HttpPost]
        public IActionResult LoadMap(string name)
        {
            var map = mapFeed.Maps
                .FirstOrDefault(t => t.City == name);

            //return View("MapInIframe", map);

            if (map == null)
                return Redirect("/");
            else
                return Redirect(map.URL);
        }
    }
}
