using Microsoft.AspNetCore.Mvc.RazorPages;
using PoGo.Web.Dto;
using PoGo.Web.Logic;
using System.Collections.Generic;

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
    }
}