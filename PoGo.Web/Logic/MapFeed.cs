using PoGo.Web.Dto;
using System.Collections.Generic;

namespace PoGo.Web.Logic
{
    public class MapFeed
    {
        public IList<MapInfo> Maps { get; set; }

        public MapFeed()
        {
            Maps = new[]
            {
                new MapInfo
                {
                    City = "Praha",
                    Image = "/images/maps/praha.png",
                    Port = 5000,
                },
                new MapInfo
                {
                    City = "Frýdek - Místek",
                    Image = "/images/maps/fm.png",
                    Port = 5010,
                },
                new MapInfo
                {
                    City = "Frýdlant nad Ostravicí",
                    Image = "/images/maps/fno.png",
                    Port = 666,
                }
            };
        }
    }
}
