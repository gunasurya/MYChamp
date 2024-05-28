using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace MYChamp.Pages
{
    public class SuccessModel : PageModel
    {
        [TempData]
        public string SuccessMessage { get; set; }

        public void OnGet()
        {
        }
    }
}
