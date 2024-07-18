using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MYChamp.DbContexts;
using MYChamp.Models;
using System.Threading.Tasks;

namespace MYChamp.Pages.ItemRegistation  // Corrected namespace to match folder structure
{
    public class IndexModel : PageModel
    {
        private readonly MYChampDbContext _dbContext;

        // Constructor injection of DbContext
        public IndexModel(MYChampDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // BindProperty for form fields
        [BindProperty]
        public Item items { get; set; }

        // GET handler
        public void OnGet()
        {
        }

        // POST handler
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            // Check if item with same name already exists
            var existingItem = await _dbContext.Items
                .FirstOrDefaultAsync(i => i.ItemName == items.ItemName);

            if (existingItem != null)
            {
                ModelState.AddModelError("Items.ItemName", "Item name must be unique.");
                return Page();
            }

            // Add new item to database
            _dbContext.Items.Add(items);
            await _dbContext.SaveChangesAsync();


            return RedirectToPage("Index");
        }
    }
}
