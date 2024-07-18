using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MYChamp.DbContexts;
using MYChamp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace MYChamp.Pages.InvoicePage
{
    public class UpdateInvoiceModel : PageModel
    {
        private readonly MYChampDbContext _dbContext;

        public UpdateInvoiceModel(MYChampDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [BindProperty]
        public Invoice Invoice { get; set; }

        public async Task<IActionResult> OnGetAsync(int id)
        {
            Invoice = await _dbContext.invoices.FirstOrDefaultAsync(i => i.Id == id);

            if (Invoice == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var invoiceToUpdate = await _dbContext.invoices.FirstOrDefaultAsync(i => i.Id == Invoice.Id);

            if (invoiceToUpdate == null)
            {
                return NotFound();
            }

            invoiceToUpdate.PaymentMode = Invoice.PaymentMode;
            invoiceToUpdate.PaymentDate = Invoice.PaymentDate;
            invoiceToUpdate.IsSettled = true;

            await _dbContext.SaveChangesAsync();

            return RedirectToPage("/InvoicePage/SettledInvoices");
        }
    }
}
