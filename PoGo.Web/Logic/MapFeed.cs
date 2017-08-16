using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using PoGo.Web.Dto;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PoGo.Web.Logic
{
    public class MapFeed
    {
        const string MapFileName = "Configuration/maps.json";
        private readonly IHostingEnvironment hostingEnvironment;

        public IReadOnlyDictionary<string, MapInfoDto> CityToMapDictionary { get; set; }
        public IList<MapInfoDto> Maps { get; set; }

        public MapFeed(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
            Maps = GetMaps();
            CityToMapDictionary = Maps.ToDictionary(t => t.City, t => t);
        }

        IList<MapInfoDto> GetMaps()
        {
            string mapFileName = hostingEnvironment.ContentRootFileProvider.GetFileInfo(MapFileName).PhysicalPath;
            var content = File.ReadAllText(mapFileName);
            return JsonConvert.DeserializeObject<IList<MapInfoDto>>(content);
        }
    }
}
