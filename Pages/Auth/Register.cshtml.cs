using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MYChamp.Models.Authentication;

namespace MYChamp.Pages.Auth
{
    [BindProperties]
    public class RegisterModel : PageModel
    {
        public Register Register { get; set; }

        public bool RegistrationSuccessful { get; set; }
        public void OnGet()
        {
        }
    }
}
