using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using PoGo.Web.Logic;
using PoGo.Web.Models.MapModels;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace PoGo.Web.Controllers
{
    public class MapController : Controller
    {
        private readonly MapFeed mapFeed;
        private readonly ILogger<MapController> logger;

        public MapController(MapFeed mapFeed, ILogger<MapController> logger)
        {
            this.mapFeed = mapFeed;
            this.logger = logger;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult LoadMap(string name)
        {
            if (mapFeed.CityToMapDictionary.TryGetValue(name, out var map))
                return Redirect(map.URL);
            else
                return Redirect("/");
        }

        MapRegistrationModel GetMapRegistrationModel(MapRegistrationModel model = null)
        {
            model = model ?? new MapRegistrationModel();

            model.Cities = mapFeed.Maps
                .Where(t => t.NeedsRegistration)
                .OrderBy(t => t.City)
                .Select(t => new SelectListItem
                {
                    Text = t.City,
                    Value = t.City,
                });

            return model;
        }

        public IActionResult Register()
        {
            var model = GetMapRegistrationModel();
            return View("Register", model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(MapRegistrationModel model)
        {
            const string AccountsFilePath = "Accounts.txt";

            if (!ModelState.IsValid)
            {
                model = GetMapRegistrationModel(model);
                return View("Register", model);
            }

            logger.LogInformation($"Map registration: {model.City}, {model.Username}, ****, {model.DiscordUsername} [{Request.HttpContext.Connection.RemoteIpAddress}]");
            using (var sw = new StreamWriter(AccountsFilePath, true))
                await sw.WriteLineAsync($"{model.City},{model.Username},{model.Password},{model.DiscordUsername}");

            return View("RegistrationFinished");
        }
    }
}
