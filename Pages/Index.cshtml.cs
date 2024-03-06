using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MYChamp.DbContexts;
using MYChamp.Models;

namespace MYChamp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly MYChampDbContext _db;

        public IndexModel(ILogger<IndexModel> logger, [FromServices] MYChampDbContext db)
        {
            _logger = logger;
            _db = db;
        }
        public List<VisitUsInformationModel> VisitUs {  get; set; }
        public void OnGet()
        {
            VisitUs = _db.VisitUsInformation.ToList();
        }
    }
}
