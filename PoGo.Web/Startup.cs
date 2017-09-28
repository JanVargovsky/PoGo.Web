using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Razor.Internal;
using Microsoft.AspNetCore.Razor.Language;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Logging;
using PoGo.Web.Identity;
using PoGo.Web.Logic;
using PoGo.Web.Models;
using System;
using System.IO;

namespace PoGo.Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>();

            services.AddScoped<ExternalSignInManager<ApplicationUser>>();

            services.AddAuthentication()
                .AddGoogle(googleOptions =>
                {
                    //googleOptions.CallbackPath = $"/Account/{nameof(Controllers.AccountController.ExternalLoginCallback)}";
                    googleOptions.ClientId = Configuration["Authentication:Google:ClientId"];
                    googleOptions.ClientSecret = Configuration["Authentication:Google:ClientSecret"];
                });

            services.AddMvc();
            //services.Configure<MvcOptions>(options => options.Filters.Add(new RequireHttpsAttribute()));

            services.AddLogging();

            services.AddSingleton<IFileProvider>(new PhysicalFileProvider(Directory.GetCurrentDirectory()));

            // workaround for IFileProvider
            // https://github.com/aspnet/Mvc/issues/6479
            services.AddSingleton<RazorProject>(s =>
            {
                return new FileProviderRazorProject(s.GetRequiredService<IRazorViewEngineFileProviderAccessor>());
            });

            // https://github.com/aspnet/Hosting/issues/793
            //services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddSingleton<CarouselFeed>();
            services.AddSingleton<FAQFeed>();
            services.AddSingleton<MapFeed>();
            services.AddSingleton<LinkShareFeed>();
            services.AddSingleton<DiscordFeed>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            //var options = new RewriteOptions()
            //    .AddRedirectToHttps();
            //app.UseRewriter(options);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                loggerFactory.AddFile(Configuration.GetSection("Logging"));
            }

            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseStatusCodePagesWithReExecute("/Error/{0}");

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
