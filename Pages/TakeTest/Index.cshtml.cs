using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MYChamp.DbContexts;
using MYChamp.Models;

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

		public async Task<IActionResult> OnGet(int testId)
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

			return Page();
		}
		public async Task<IActionResult> OnPostAsync()
		{
			if (!ModelState.IsValid)
			{
				return Page();
			}
			List<int> selectedQuestionIds = new List<int>();
			List<int> selectedOptionIds = new List<int>();
			foreach (var question in TestAttemptModel.QuestionsList)
			{
				selectedQuestionIds.Add(question.Id);
				selectedOptionIds.Add(int.Parse(question.SelectedOption));
			}


			string selectedQuestionIdsString = string.Join(",", selectedQuestionIds);
			string selectedOptionIdsString = string.Join(",", selectedOptionIds);
			var userIdString = _httpContextAccessor.HttpContext.Session.GetString("UserId");
			if (string.IsNullOrEmpty(userIdString))
			{
				// Handle the case where the user ID is not found in the session
				ModelState.AddModelError(string.Empty, "User is not logged in properly.");
				return Page();
			}
			TestAttemptModel.UserId = int.Parse(userIdString);
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
			return RedirectToPage("/Success"); // Redirect to a success page

		}
	}



}

