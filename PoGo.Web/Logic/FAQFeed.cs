using Microsoft.AspNetCore.Hosting;
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

        public IList<Question> Questions { get; set; }

        public FAQFeed(IHostingEnvironment hostingEnvironment)
        {
            this.hostingEnvironment = hostingEnvironment;
            Questions = GetQuestions();
            RegisterRefresh();
        }

        void RegisterRefresh()
        {
            var token = hostingEnvironment.ContentRootFileProvider.Watch(FAQFileName);
            token.RegisterChangeCallback(state =>
            {
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
