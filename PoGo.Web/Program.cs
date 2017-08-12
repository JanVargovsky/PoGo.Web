using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;

namespace PoGo.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls("http://localhost:60000")
                .Build();
    }
}
