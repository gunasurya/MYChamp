using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MYChamp.AuthModel;
using MYChamp.DbContexts;
using System;
using System.Linq;

namespace MYChamp.Pages.Consultee_Pages
{
    public class ConsulteeLogModel : PageModel
    {
        private readonly MYChampDbContext _db;

        [BindProperty]
        public ConsulteeLog ConsulteeLog { get; set; }

        public IQueryable<ConsulteeRegister> Consultees { get; set; }

        public ConsulteeLogModel(MYChampDbContext db)
        {
            _db = db;
            ConsulteeLog = new ConsulteeLog
            {
                FromDateTime = DateTime.Now,
                GenerateReport = "Generate"
            };
        }

        public void OnGet()
        {
            Consultees = _db.consultees;
        }

        public IActionResult OnPost()
        {
            if (ModelState.IsValid)
            {
                var duration = (ConsulteeLog.ToDateTime - ConsulteeLog.FromDateTime).TotalMinutes;
                if (duration <= 0)
                {
                    ModelState.AddModelError(string.Empty, "Select a correct To DateTime.");
                    Consultees = _db.consultees;
                    return Page();
                }

                ConsulteeLog.Duration = duration;
                var consultee = _db.consultees.Find(ConsulteeLog.ConsulteeId);
                if (consultee != null)
                {
                    var costPerHour = consultee.costPerHour ?? 0.0;
                    ConsulteeLog.Cost = (ConsulteeLog.Duration / 60) * costPerHour;
                }

                ConsulteeLog.Date = DateTime.UtcNow;
                ConsulteeLog.FromDateTime = DateTime.SpecifyKind(ConsulteeLog.FromDateTime, DateTimeKind.Utc);
                ConsulteeLog.ToDateTime = DateTime.SpecifyKind(ConsulteeLog.ToDateTime, DateTimeKind.Utc);
                ConsulteeLog.GenerateReport = "Generate";

                _db.consulteelog.Add(ConsulteeLog);
                _db.SaveChanges();

                return new JsonResult(new { success = true });
            }
            else
            {
                Console.WriteLine("Model state is invalid");
                foreach (var modelState in ModelState.Values)
                {
                    foreach (var error in modelState.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.ErrorMessage);
                        Console.WriteLine(error.ErrorMessage);
                    }
                }
            }

            Consultees = _db.consultees;
            return new JsonResult(new { success = false });
        }
    }
}
