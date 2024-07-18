using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MYChamp.DbContexts;
using MYChamp.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text.Json;

namespace MYChamp.Pages.InvoicePage
{
    public class IndexModel : PageModel
    {
        private readonly MYChampDbContext _dbContext;

        // Constructor injection of DbContext
        public IndexModel(MYChampDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IList<Item> Items { get; set; }

        [BindProperty]
        public string customername { get; set; }

        [BindProperty]
        public string location { get; set; }

        [BindProperty]
        public Dictionary<string, decimal> dict { get; set; } = new Dictionary<string, decimal>();

        [BindProperty]
        public decimal discount { get; set; }

        [BindProperty]
        public decimal? referraldiscount { get; set; }

        [BindProperty]
        public DateTime? paymentdate { get; set; }

        [BindProperty]
        public string? paymentmode { get; set; }

        [BindProperty]
        public string SelectedItems { get; set; }

        public async Task OnGetAsync()
        {
            Items = await _dbContext.Items.ToListAsync();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!string.IsNullOrEmpty(SelectedItems))
            {
                dict = JsonSerializer.Deserialize<Dictionary<string, decimal>>(SelectedItems);
            }

            Console.WriteLine("post request dict data");
            foreach (var kvp in dict)
            {
                string key = kvp.Key;
                decimal value = kvp.Value;
                Console.WriteLine($"Key: {key}, Value: {value}");
            }

            if (!ModelState.IsValid)
            {
                Console.WriteLine("model state is invakid");
                //  return Page();
            }

            var utcNow = DateTime.UtcNow;

            decimal subTotal = dict.Values.Sum();
            decimal discountAmount = subTotal * (discount / 100);
            decimal totalPrice = subTotal - discountAmount - (referraldiscount ?? 0);
            decimal gstPercentage = (await _dbContext.GSTs.FirstOrDefaultAsync())?.Percentage ?? 0.18M;
            decimal gst = totalPrice * gstPercentage;
            decimal grandTotal = totalPrice + gst;

            var itemsPurchased = string.Join(",", dict.Keys);
            var itemsCosts = string.Join(",", dict.Values.Select(v => v.ToString()));

            var invoice = new Invoice
            {
                CustomerName = customername,
                Location = location,
                Date = utcNow,
                Discount = discount,
                ReferralDiscount = referraldiscount,
                CompanyGSTNumber = "YOUR_GST_NUMBER",
                SubTotal = subTotal,
                TotalPrice = totalPrice,
                GST = gst,
                GrandTotal = grandTotal,
                PaymentDate = paymentdate.HasValue ? paymentdate.Value.ToUniversalTime() : (DateTime?)null,
                PaymentMode = paymentmode ?? string.Empty,
                Itemspurchased = itemsPurchased,
                Itemscosts = itemsCosts,
                IsSettled = paymentdate.HasValue || !string.IsNullOrEmpty(paymentmode)
            };

            invoice.InvoiceNumber = $"TCINTP{utcNow:yyMMdd}-{_dbContext.invoices.Count(i => i.Date.Date == utcNow.Date) + 1:00}";

            _dbContext.invoices.Add(invoice);
            await _dbContext.SaveChangesAsync();

            return RedirectToPage("/InvoicePage/InvoiceDisplay", new { id = invoice.Id });
        }
    }
}