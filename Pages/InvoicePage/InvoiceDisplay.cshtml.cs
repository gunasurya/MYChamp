using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MYChamp.DbContexts;
using MYChamp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MYChamp.Pages.InvoicePage
{
    public class InvoiceDisplayModel : PageModel
    {
        private readonly MYChampDbContext _dbContext;

        public InvoiceDisplayModel(MYChampDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Invoice Invoice { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            // Retrieve the invoice based on the provided id
            Invoice = await _dbContext.invoices.FirstOrDefaultAsync(i => i.Id == id);

            if (Invoice == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}

