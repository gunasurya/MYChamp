using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MYChamp.Models.Authentication;

namespace MYChamp.Pages.Auth
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public Login login { get; set; }
        public IActionResult OnGet()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToPage("/Index");
            }
            return Page();
        }
    }
}
