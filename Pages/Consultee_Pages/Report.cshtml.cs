using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MYChamp.AuthModel;
using MYChamp.DbContexts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MYChamp.Pages.Consultee_Pages
{
    public class ReportModel : PageModel
    {
        private readonly MYChampDbContext _db;
        public List<ConsulteeLog> Allbills { get; set; }

        public ReportModel(MYChampDbContext db)
        {
            _db = db;
            Allbills = _db.consulteelog.ToList();
        }

        public void OnGet()
        {
            Console.WriteLine(Allbills.Count + " The count is ");
        }

        public JsonResult OnGetTotalDurationCostData()
        {
            var result = Allbills.GroupBy(log => log.Date.Date)
                                 .Select(group => new
                                 {
                                     Date = group.Key,
                                     TotalDuration = group.Sum(log => log.Duration),
                                     TotalCost = group.Sum(log => log.Cost)
                                 })
                                 .ToList();
            return new JsonResult(result);
        }

        public JsonResult OnGetUserDurationCostData()
        {
            var result = Allbills.GroupBy(log => log.ConsulteeId)
                                 .Select(group => new
                                 {
                                     ConsulteeId = group.Key,
                                     TotalDuration = group.Sum(log => log.Duration),
                                     TotalCost = group.Sum(log => log.Cost)
                                 })
                                 .ToList();
            return new JsonResult(result);
        }

        public JsonResult OnGetDailyUsageData()
        {
            var result = Allbills.GroupBy(log => log.Date.Date)
                                 .Select(group => new
                                 {
                                     Date = group.Key,
                                     TotalUsers = group.Select(log => log.ConsulteeId).Distinct().Count()
                                 })
                                 .ToList();
            return new JsonResult(result);
        }

        public JsonResult OnGetInsightfulData()
        {
            var result = new List<object>
            {
                new { label = "Example 1", value = 10 },
                new { label = "Example 2", value = 20 },
                new { label = "Example 3", value = 30 },
                new { label = "Example 4", value = 40 }
            };
            return new JsonResult(result);
        }
    }
}
