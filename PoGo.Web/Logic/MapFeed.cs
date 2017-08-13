using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using PoGo.Web.Dto;
using System.Collections.Generic;
using System.IO;

namespace PoGo.Web.Logic
{
    public class MapFeed
    {
        const string MapFileName = "Configuration/maps.json";
        private readonly IHostingEnvironment hostingEnvironment;

        public IList<MapInfo> Maps { get; set; }

        public MapFeed(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
            Maps = GetMaps();
        }

        IList<MapInfo> GetMaps()
        {
            string mapFileName = hostingEnvironment.ContentRootFileProvider.GetFileInfo(MapFileName).PhysicalPath;
            var content = File.ReadAllText(mapFileName);
            return JsonConvert.DeserializeObject<IList<MapInfo>>(content);
        }
    }
}
