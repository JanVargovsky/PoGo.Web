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
                    City = "Praha - Pokemoni",
                    Image = "/images/maps/prague.png",
                    URL = "http://nagas.cz:5000",
                },
                new MapInfo
                {
                    City = "Praha - Raidy",
                    Image = "/images/maps/prague-raids.png",
                    URL = "http://nagas.cz:5001",
                },
                new MapInfo
                {
                    City = "Frýdek - Místek",
                    Image = "/images/maps/fm2.png",
                    URL = "http://nagas.cz:5010",
                },
                new MapInfo
                {
                    City = "Frýdlant nad Ostravicí",
                    Image = "/images/maps/fno2.png",
                    URL = "http://nagas.cz:666",
                }
            };
        }
    }
}
