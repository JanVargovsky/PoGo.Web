using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PoGo.Web.Logic
{
    public class CarouselFeed
    {
        const string SubPath = "images/carousel";
        private readonly IHostingEnvironment hostingEnvironment;

        public IList<string> Images { get; set; }

        public CarouselFeed(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;

            Images = GetImagePaths();
            RegisterRefresh();
        }

        void RegisterRefresh()
        {
            var token = hostingEnvironment.WebRootFileProvider.Watch(SubPath + "*");
            token.RegisterChangeCallback(o =>
            {
                Images = GetImagePaths();
                RegisterRefresh();
            }, null);
        }

        IList<string> GetImagePaths()
        {
            return hostingEnvironment.WebRootFileProvider.GetDirectoryContents(SubPath)
                .Select(t => Path.Combine(SubPath, t.Name))
                .ToList();
        }
    }
}
