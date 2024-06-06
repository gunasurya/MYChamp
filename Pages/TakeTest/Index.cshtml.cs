using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MYChamp.DbContexts;
using MYChamp.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MYChamp.Pages.TakeTest
{
    public class IndexModel : PageModel
    {
        private readonly MYChampDbContext _dbContext;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public IndexModel(MYChampDbContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _dbContext = dbContext;
            _httpContextAccessor = httpContextAccessor;
        }

        [BindProperty]
        public TestAttemptViewModel TestAttemptModel { get; set; }

        [BindProperty]
        public int CurrentQuestionIndex { get; set; }
        [BindProperty]
        public int TestId { get; set; }

        public async Task<IActionResult> OnGetAsync(int testId, int? questionIndex)
        {
            TestAttemptModel = new TestAttemptViewModel
            {
                TestId = testId,
                QuestionsList = new List<QuestionModel>()
            };

            var questions = await _dbContext.personality_questions.ToListAsync();

            TestAttemptModel.QuestionsList = questions.Select(q => new QuestionModel
            {
                Id = q.question_id,
                Question = q.question_text,
                Options = q.options.Split("#*#").ToList()
            }).ToList();

            CurrentQuestionIndex = questionIndex ?? 0;

            return Page();
        }

        public IActionResult OnPostNext()
        {
            if (CurrentQuestionIndex < TestAttemptModel.QuestionsList.Count - 1)
            {
                CurrentQuestionIndex++;
            }

            return Page();
        }

        public IActionResult OnPostPrevious()
        {
            if (CurrentQuestionIndex > 0)
            {
                CurrentQuestionIndex--;
            }

            return Page();
        }

        public async Task<IActionResult> OnPostSubmitAsync()
        {
            Console.WriteLine("entered");
            if (!ModelState.IsValid)
            {
                foreach (var modelError in ViewData.ModelState.Values.SelectMany(v => v.Errors))
                {
                    Console.Write(modelError.ErrorMessage);
                }
                //return Page();
            }

            var selectedQuestionIds = new List<int>();
            var selectedOptionIds = new List<int>();

            foreach (var question in TestAttemptModel.QuestionsList)
            {
                Console.WriteLine(question.Id + " " + question.Options);
                if (string.IsNullOrEmpty(question.SelectedOption))
                {
                    ModelState.AddModelError(string.Empty, $"Please select an option for question {question.Id}");
                    //return Page();
                }

                selectedQuestionIds.Add(question.Id);
                if (!int.TryParse(question.SelectedOption, out int selectedOption))
                {
                    ModelState.AddModelError(string.Empty, $"Invalid option selected for question {question.Id}");
                    //return Page();
                }
                selectedOptionIds.Add(selectedOption);
            }

            var selectedQuestionIdsString = string.Join(",", selectedQuestionIds);
            var selectedOptionIdsString = string.Join(",", selectedOptionIds);

            var userIdString = _httpContextAccessor.HttpContext.Session.GetString("UserId");
            if (string.IsNullOrEmpty(userIdString))
            {
                ModelState.AddModelError(string.Empty, "User is not logged in properly.");
                return Page();
            }

            TestAttemptModel.UserId = int.Parse(userIdString);
            Console.WriteLine(selectedQuestionIdsString + "" + selectedOptionIdsString);
            var testAttempt = new TestAttemptModel
            {
                TestId = TestAttemptModel.TestId,
                UserId = TestAttemptModel.UserId,
                selectedQuestions = selectedQuestionIdsString,
                selectedOptions = selectedOptionIdsString
            };

            _dbContext.TestAttempts.Add(testAttempt);
            await _dbContext.SaveChangesAsync();
            TempData["SuccessMessage"] = "Your test attempt has been successfully recorded";
            return RedirectToPage("/Success");
        }
    }
}
