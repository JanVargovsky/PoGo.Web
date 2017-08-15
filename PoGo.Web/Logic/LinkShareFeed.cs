using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using PoGo.Web.Dto;
using System.Collections.Generic;
using System.IO;

namespace PoGo.Web.Logic
{
    public class LinkShareFeed
    {
        const string LinksFileName = "Configuration/links.json";
        private readonly IHostingEnvironment hostingEnvironment;

        public IList<LinkShareDto> UsefulInfos { get; set; }

        public LinkShareFeed(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
            UsefulInfos = GetUsefulInfos();
        }

        IList<LinkShareDto> GetUsefulInfos()
        {
            string linksFileName = hostingEnvironment.ContentRootFileProvider.GetFileInfo(LinksFileName).PhysicalPath;
            var content = File.ReadAllText(linksFileName);
            return JsonConvert.DeserializeObject<IList<LinkShareDto>>(content);
        }
    }
}
