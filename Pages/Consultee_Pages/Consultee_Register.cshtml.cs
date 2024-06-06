using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MYChamp.AuthModel;
using MYChamp.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MYChamp.Pages.Consultee_Pages
{
    public class Consultee_RegisterModel : PageModel
    {
        private readonly MYChampDbContext _db;

        [BindProperty]
        public ConsulteeRegister _register { get; set; }
        public List<Currency> Currencies { get; set; }

        public Consultee_RegisterModel(MYChampDbContext db)
        {
            _db = db;
            _register = new ConsulteeRegister();
            Currencies = new List<Currency>();
        }

        public void OnGet()
        {
            // Query the database and fetch currencies
            Currencies = _db.currency.ToList();
        }

        public IActionResult OnPost()
        {
            var result = _db.consultees.FirstOrDefault(u => u.consulteeName == _register.consulteeName && u.consulteeEmail == _register.consulteeEmail);
            if (result != null)
            {
                ModelState.AddModelError(string.Empty, "Username already present");
                Currencies = _db.currency.ToList();
                return Page();
            }
            if (ModelState.IsValid)
            {
                // Convert BillingDate to UTC
                if (_register.BillingDate.HasValue)
                {
                    _register.BillingDate = DateTime.SpecifyKind(_register.BillingDate.Value, DateTimeKind.Utc);
                }

                var consultee_info = new ConsulteeRegister
                {
                    consulteeId = _register.consulteeId,
                    consulteeName = _register.consulteeName,
                    consulteeEmail = _register.consulteeEmail,
                    costPerHour = _register.costPerHour,
                    UnitOfCurrency = _register.UnitOfCurrency,
                    BillingDate = _register.BillingDate,
                };

                try
                {
                    _db.consultees.Add(consultee_info);
                    _db.SaveChanges();
                    TempData["success"] = "yes";  // Set TempData["success"]
                    return RedirectToPage("/");
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError(string.Empty, "An error occurred while saving the data: " + ex.Message);
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Some error occurred. Please check your input and try again.");
            }

            Currencies = _db.currency.ToList();
            return Page();
        }
    }
}
