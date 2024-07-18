using Microsoft.AspNetCore.Mvc.RazorPages;
using MYChamp.DbContexts;
using MYChamp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MYChamp.Pages.InvoicePage
{
    public class UnsettledInvoicesModel : PageModel
    {
        private readonly MYChampDbContext _dbContext;

        public UnsettledInvoicesModel(MYChampDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Invoice> UnsettledInvoices { get; set; }

        public async Task OnGetAsync()
        {
            UnsettledInvoices = await _dbContext.invoices.Where(i => !i.IsSettled).ToListAsync();
        }
    }
}
