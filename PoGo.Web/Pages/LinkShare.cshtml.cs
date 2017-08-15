using Microsoft.AspNetCore.Mvc.RazorPages;
using PoGo.Web.Dto;
using PoGo.Web.Logic;
using System.Collections.Generic;

namespace PoGo.Web.Pages
{
    public class LinkShareModel : PageModel
    {
        private readonly LinkShareFeed linkShareFeed;

        public IList<LinkShareDto> Links { get; set; }

        public LinkShareModel(LinkShareFeed linkShareFeed)
        {
            this.linkShareFeed = linkShareFeed;
        }

        public void OnGet()
        {
            Links = linkShareFeed.UsefulInfos;
        }
    }
}