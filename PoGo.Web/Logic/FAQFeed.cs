using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using PoGo.Web.Dto;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace PoGo.Web.Logic
{
    public class FAQFeed
    {
        const string FAQFileName = "Configuration/faq.json";
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly ILogger<FAQFeed> logger;

        public IList<QuestionDto> Questions { get; set; }

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

        IList<QuestionDto> GetQuestions()
        {
            string faqFileName = hostingEnvironment.ContentRootFileProvider.GetFileInfo(FAQFileName).PhysicalPath;
            var content = File.ReadAllText(faqFileName);
            return JsonConvert.DeserializeObject<IList<QuestionDto>>(content);
        }

        IEnumerable<QuestionDto> LoadDummyQuestions() =>
            Enumerable.Range(0, 10)
            .Select(i => new QuestionDto { Title = $"Stupid question#{i}", Answer = $"Answer#{i}" });
    }
}
