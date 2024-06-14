using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MYChamp.DbContexts;
using System;
using System.Linq;

namespace MYChamp.Pages.Consultee_Pages
{
    public class ReportModel : PageModel
    {
        private readonly MYChampDbContext _db;

        public ReportModel(MYChampDbContext db)
        {
            _db = db;
        }

        
        public void OnGet()
        {
            Console.WriteLine("OnGet method called.");
        }

       
        public JsonResult OnGetUsers()
        {
            var users = _db.consultees
                           .Select(user => new { Id = user.consulteeId, Name = user.consulteeName })
                           .Distinct()
                           .ToList();
            return new JsonResult(users);
        }

        // Handler to retrieve years
        public JsonResult OnGetYears()
        {
            var years = _db.consulteelog
                           .Select(log => log.Date.Year)
                           .Distinct()
                           .OrderBy(year => year)
                           .ToList();
            return new JsonResult(years);
        }

        // Handler to retrieve monthly duration and cost data
        public JsonResult OnGetMonthlyDurationCostData(int userId, int year)
        {
            var result = _db.consulteelog
                             .Where(log => log.ConsulteeId == userId && log.Date.Year == year)
                             .GroupBy(log => log.Date.Month)
                             .Select(group => new
                             {
                                 Month = new DateTime(year, group.Key, 1).ToString("MMMM"),
                                 TotalDuration = group.Sum(log => log.Duration),
                                 TotalCost = group.Sum(log => log.Cost)
                             })
                             .ToList();
            return new JsonResult(result);
        }

        // Handler to retrieve monthly bill status data
        public JsonResult OnGetMonthlyBillStatusData(int userId, int year)
        {
            var result = _db.consulteelog
                             .Where(log => log.ConsulteeId == userId && log.Date.Year == year)
                             .GroupBy(log => log.Date.Month)
                             .Select(group => new
                             {
                                 Month = new DateTime(year, group.Key, 1).ToString("MMMM"),
                                 PaidAmount = group.Where(log => log.GenerateReport == "Generate").Sum(log => log.Cost),
                                 UnpaidAmount = group.Where(log => log.GenerateReport != "Generate").Sum(log => log.Cost)
                             })
                             .ToList();
            return new JsonResult(result);
        }
    }
}
