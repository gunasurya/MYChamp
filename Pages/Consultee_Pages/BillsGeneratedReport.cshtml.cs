using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MYChamp.Controller;
using MYChamp.DbContexts;
using MYChamp.AuthModel; // Ensure you include this namespace
using System;
using System.Collections.Generic;
using System.Linq;

namespace MYChamp.Pages.Consultee_Pages
{
    public class BillsGeneratedReportModel : PageModel
    {
        private readonly MYChampDbContext _db;
        private readonly SendEmail _sendEmail; // Injected SendEmail service

        [BindProperty]
        public string ConsulteeName { get; set; }

        [BindProperty]
        public List<BillsGeneratedReportViewModel> BillsGeneratedReport { get; set; }

        [BindProperty]
        public List<BillsGeneratedReportViewModel> BillsPendingReport { get; set; }
        [BindProperty]
        public List<BillsGeneratedReportViewModel> AllBillsGeneratedReport { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FromDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? ToDate { get; set; }

        // Constructor with dependency injection
        public BillsGeneratedReportModel(MYChampDbContext db, SendEmail sendEmail)
        {
            _db = db;
            _sendEmail = sendEmail; // Initialize the SendEmail service
        }

        public void OnGet()
        {
            LoadData();
        }

        private void LoadData()
        {
            BillsGeneratedReport = new List<BillsGeneratedReportViewModel>();
            BillsPendingReport = new List<BillsGeneratedReportViewModel>();

            AllBillsGeneratedReport = new List<BillsGeneratedReportViewModel>();

            if (FromDate.HasValue && ToDate.HasValue)
            {
                DateTime fromDateUtc = DateTime.SpecifyKind(FromDate.Value, DateTimeKind.Utc);
                DateTime toDateUtc = DateTime.SpecifyKind(ToDate.Value, DateTimeKind.Utc);

                BillsGeneratedReport = (from log in _db.consulteelog
                                        join consultee in _db.consultees
                                        on log.ConsulteeId equals consultee.consulteeId
                                        where log.FromDateTime >= fromDateUtc && log.ToDateTime <= toDateUtc && log.GenerateReport == "Generate"
                                        group log by new { log.ConsulteeId, consultee.consulteeName } into g
                                        select new BillsGeneratedReportViewModel
                                        {
                                            ConsulteeName = g.Key.consulteeName,
                                            FromDateTime = g.Min(x => x.FromDateTime),
                                            ToDateTime = g.Max(x => x.ToDateTime),
                                            Duration = g.Sum(x => x.Duration),
                                            Cost = g.Sum(x => x.Cost),
                                            GenerateReport = g.Min(x => x.GenerateReport)
                                        }).ToList();
                BillsPendingReport = (from log in _db.consulteelog
                                      join consultee in _db.consultees
                                      on log.ConsulteeId equals consultee.consulteeId
                                      where log.FromDateTime >= fromDateUtc && log.ToDateTime <= toDateUtc && log.GenerateReport == "Payment Pending"
                                      group log by new { log.ConsulteeId, consultee.consulteeName } into g
                                      select new BillsGeneratedReportViewModel
                                      {
                                          ConsulteeName = g.Key.consulteeName,
                                          FromDateTime = g.Min(x => x.FromDateTime),
                                          ToDateTime = g.Max(x => x.ToDateTime),
                                          Duration = g.Sum(x => x.Duration),
                                          Cost = g.Sum(x => x.Cost),
                                          GenerateReport = g.Max(x => x.GenerateReport)
                                      }).ToList();

                BillsGeneratedReport = BillsGeneratedReport.Concat(BillsPendingReport).ToList();

                AllBillsGeneratedReport = (from log in _db.consulteelog
                                           join consultee in _db.consultees
                                           on log.ConsulteeId equals consultee.consulteeId
                                           where log.FromDateTime >= fromDateUtc && log.ToDateTime <= toDateUtc
                                           select new BillsGeneratedReportViewModel
                                           {
                                               ConsulteeName = consultee.consulteeName,
                                               FromDateTime = log.FromDateTime,
                                               ToDateTime = log.ToDateTime,
                                               Duration = log.Duration,
                                               Cost = log.Cost,
                                               GenerateReport = log.GenerateReport
                                           }).ToList();
            }
        }

        public IActionResult OnPostSendEmail()
        {
            LoadData();

            if (AllBillsGeneratedReport == null || !AllBillsGeneratedReport.Any())
            {
                Console.WriteLine("No bills are presented.");
                TempData["response"] = "Error: No bills found.";
                return Page();
            }

            var consulteeName = ConsulteeName;
            Console.WriteLine(consulteeName + " this is consultee name");

            var consulteeBills = AllBillsGeneratedReport
                .Where(x => x.ConsulteeName.Equals(consulteeName, StringComparison.OrdinalIgnoreCase))
                .ToList();
            Console.WriteLine(consulteeBills.Count + " count of records is ");

            if (!consulteeBills.Any())
            {
                Console.WriteLine("No bills found for the specified consultee.");
                TempData["response"] = "Error: No bills found for the specified consultee.";
                return Page();
            }


            var subject = "Your Bills Report";
            var body = $"Hello {consulteeName},\n\nHere is your bills report:\n\n";
            foreach (var bill in consulteeBills)
            {
                body += $"From: {bill.FromDateTime}, To: {bill.ToDateTime}, Duration: {bill.Duration} minutes, Cost: {bill.Cost} USD\n";
            }

            try
            {
                _sendEmail.Send(consulteeName, subject, body);


                foreach (var bill in consulteeBills)
                {
                    var log = _db.consulteelog.FirstOrDefault(l => l.ConsulteeId == (from c in _db.consultees where c.consulteeName == bill.ConsulteeName select c.consulteeId).FirstOrDefault()
                        && l.FromDateTime == bill.FromDateTime
                        && l.ToDateTime == bill.ToDateTime);

                    if (log != null)
                    {
                        log.GenerateReport = "Payment Pending";
                        _db.consulteelog.Update(log);
                    }
                }

                _db.SaveChanges();
                TempData["response"] = "Success";
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error sending email: " + ex.Message);
                TempData["response"] = "Error: " + ex.Message;
            }

            LoadData();

            return Page();
        }

        public class BillsGeneratedReportViewModel
        {
            public DateTime FromDateTime { get; set; }
            public DateTime ToDateTime { get; set; }
            public double Duration { get; set; }
            public double Cost { get; set; }
            public string ConsulteeName { get; set; }
            public string GenerateReport { get; set; }
        }
    }
}