using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json;
using PoGo.Web.Dto;
using System.Collections.Generic;
using System.IO;

namespace PoGo.Web.Logic
{
    public class FAQFeed
    {
        const string FAQFileName = @"Configuration\faq.json";
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ILogger<FAQFeed> logger;

        public IList<Question> Questions { get; set; }

        public FAQFeed(IHostingEnvironment hostingEnvironment, ILogger<FAQFeed> logger)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.logger = logger;
            Questions = GetQuestions();
            RegisterRefresh();
        }

        void RegisterRefresh()
        {
            var token = hostingEnvironment.ContentRootFileProvider.Watch(FAQFileName);
            token.RegisterChangeCallback(state =>
             {
                 logger.LogInformation($"{FAQFileName} changed loading new questions");
                 Questions = GetQuestions();
                 RegisterRefresh();
             }, null);
        }

        IList<Question> GetQuestions()
        {
            string faqFileName = hostingEnvironment.ContentRootFileProvider.GetFileInfo(FAQFileName).PhysicalPath;
            var content = File.ReadAllText(faqFileName);
            return JsonConvert.DeserializeObject<IList<Question>>(content);
        }
    }
}
