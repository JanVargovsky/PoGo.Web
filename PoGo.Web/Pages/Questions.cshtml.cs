using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using PoGo.Web.Logic;
using PoGo.Web.Dto;

namespace PoGo.Web.Pages
{
    public class QuestionsModel : PageModel
    {
        private readonly FAQFeed faqFeed;

        public IEnumerable<Question> Questions { get; set; }

        public QuestionsModel(FAQFeed faqFeed)
        {
            this.faqFeed = faqFeed;
        }

        public void OnGet()
        {
            //Questions = LoadDummyQuestions();
            Questions = faqFeed.Questions;
        }

        IEnumerable<Question> LoadDummyQuestions() =>
            Enumerable.Range(0, 10)
            .Select(i => new Question { Title = $"Stupid question#{i}", Answer = $"Answer#{i}" });
    }
}