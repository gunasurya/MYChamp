using Microsoft.AspNetCore.Mvc.RazorPages;
using MYChamp.DbContexts;
using MYChamp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MYChamp.Pages.InvoicePage
{
    public class SettledInvoicesModel : PageModel
    {
        private readonly MYChampDbContext _dbContext;

        public SettledInvoicesModel(MYChampDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Invoice> SettledInvoices { get; set; }

        public async Task OnGetAsync()
        {
            SettledInvoices = await _dbContext.invoices.Where(i => i.IsSettled).ToListAsync();
        }
    }
}
