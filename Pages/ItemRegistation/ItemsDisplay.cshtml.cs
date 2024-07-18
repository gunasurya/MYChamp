using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using MYChamp.DbContexts;
using MYChamp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MYChamp.Pages.ItemRegistation  // Ensure namespace matches your project structure
{
    public class ItemsDisplayModel : PageModel
    {
        private readonly MYChampDbContext _dbContext;

        public ItemsDisplayModel(MYChampDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Item> Items { get; set; }

        public async Task OnGetAsync()
        {
            Items = await _dbContext.Items.ToListAsync();
        }

        public async Task<IActionResult> OnGetItemDetailsAsync(int id)
        {
            var item = await _dbContext.Items.FindAsync(id);
            if (item == null)
            {
                return NotFound();
            }

            return new JsonResult(item);
        }
    }
}
