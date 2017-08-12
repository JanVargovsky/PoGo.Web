using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace PoGo.Web.Pages
{
    public class QuestionsModel : PageModel
    {
        public class Question
        {
            public string Title { get; set; }
            public string Answer { get; set; }
        }

        public IEnumerable<Question> Questions { get; set; }

        public void OnGet()
        {
            Questions = LoadDummyQuestions();
        }

        IEnumerable<Question> LoadDummyQuestions() =>
            Enumerable.Range(0, 10)
            .Select(i => new Question { Title = $"Stupid question#{i}", Answer = $"Answer#{i}" });
    }
}