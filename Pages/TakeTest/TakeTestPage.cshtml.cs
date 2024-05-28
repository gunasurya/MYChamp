using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MYChamp.Controllers;
using MYChamp.DbContexts;
using MYChamp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MYChamp.Pages.TakeTest
{
    public class TakeTestPageModel : PageModel
    {
        private readonly MYChampDbContext _dbContext;
        private readonly CardRegistrationController _CardRegistrationController;

        public TakeTestPageModel(CardRegistrationController CardRegistrationController, MYChampDbContext MYChampDbContext)
        {
            _dbContext = MYChampDbContext;
            _CardRegistrationController = CardRegistrationController;
        }

        [BindProperty]
        public TestRegistrationModel TestRegistrationModel { get; set; }
        public List<TestRegistrationModel> TestList { get; set; }

        public async Task<IActionResult> OnGet()
        {
            TestList = await _dbContext.testregistration.ToListAsync();
            return Page();
        }
    }
}
